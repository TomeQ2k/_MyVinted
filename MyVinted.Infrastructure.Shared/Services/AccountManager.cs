using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailSender emailSender;
        private readonly ICryptoService cryptoService;
        private readonly IFilesManager filesManager;
        private readonly IHttpContextReader httpContextReader;

        public IConfiguration Configuration { get; }

        public AccountManager(UserManager<User> userManager, IUnitOfWork unitOfWork, IEmailSender emailSender,
            ICryptoService cryptoService, IFilesManager filesManager,
            IConfiguration configuration, IHttpContextReader httpContextReader)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.emailSender = emailSender;
            this.cryptoService = cryptoService;
            this.filesManager = filesManager;
            this.httpContextReader = httpContextReader;

            Configuration = configuration;
        }

        public async Task<User> GetCurrentUser() => await userManager.FindByIdAsync(httpContextReader.CurrentUserId) ??
                                                    throw new EntityNotFoundException("User not found");

        public async Task<bool> ChangeUsername(string newUsername)
        {
            var currentUser = await GetCurrentUser();

            return (await userManager.SetUserNameAsync(currentUser, newUsername)).Succeeded;
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            var currentUser = await GetCurrentUser();

            if (!await userManager.CheckPasswordAsync(currentUser, oldPassword))
                throw new OldPasswordInvalidException("Old password is invalid");

            return (await userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword)).Succeeded;
        }

        public async Task<bool> ChangeEmail(string newEmail, string token)
        {
            var currentUser = await GetCurrentUser();

            token = cryptoService.Decrypt(token);

            return (await userManager.ChangeEmailAsync(currentUser, newEmail, token)).Succeeded;
        }

        public async Task<bool> SendChangeEmailCallback(string newEmail)
        {
            var currentUser = await GetCurrentUser();

            var changeEmailToken = await userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);
            changeEmailToken = cryptoService.Encrypt(changeEmailToken);

            string callbackUrl =
                $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}account/changeEmail/confirm?newEmail={newEmail}&token={changeEmailToken}";

            return await emailSender.Send(EmailMessages.EmailChangeEmail(newEmail, callbackUrl));
        }

        public async Task<bool> ChangePhoneNumber(string newPhoneNumber)
        {
            var currentUser = await GetCurrentUser();

            var changePhoneNumberToken =
                await userManager.GenerateChangePhoneNumberTokenAsync(currentUser, newPhoneNumber);

            return (await userManager.ChangePhoneNumberAsync(currentUser, newPhoneNumber, changePhoneNumberToken))
                .Succeeded;
        }

        public async Task<string> SetAvatar(IFormFile avatar)
        {
            var currentUser = await GetCurrentUser();

            string filesPath = $"files/avatars/{currentUser.Id}";
            filesManager.DeleteDirectory(filesPath);

            await unitOfWork.FileRepository.DeleteFileByPath(filesPath);

            var uploadedAvatar = await filesManager.Upload(avatar, $"avatars/{currentUser.Id}");
            currentUser.SetAvatarUrl(uploadedAvatar?.Path);

            unitOfWork.FileRepository.AddFile(uploadedAvatar?.Path);

            await unitOfWork.Complete();

            return currentUser.AvatarUrl;
        }

        public async Task<bool> DeleteAvatar()
        {
            var currentUser = await GetCurrentUser();

            if (string.IsNullOrEmpty(currentUser.AvatarUrl))
                return false;

            string filesPath = $"files/avatars/{currentUser.Id}";
            filesManager.DeleteDirectory(filesPath);
            currentUser.SetAvatarUrl(null);

            await unitOfWork.FileRepository.DeleteFileByPath(filesPath);

            return await unitOfWork.Complete();
        }
    }
}