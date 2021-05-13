using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Infrastructure.Shared.Specifications;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class ResetPasswordManager : IResetPasswordManager
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly ICryptoService cryptoService;

        public IConfiguration Configuration { get; }

        public ResetPasswordManager(UserManager<User> userManager, IEmailSender emailSender, ICryptoService cryptoService, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.cryptoService = cryptoService;
            Configuration = configuration;
        }

        public async Task<bool> ResetPassword(string email, string newPassword, string token)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new EntityNotFoundException("User not found");

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException();

            (token, newPassword) = (cryptoService.Decrypt(token), cryptoService.Decrypt(newPassword));

            return (await userManager.ResetPasswordAsync(user, token, newPassword)).Succeeded;
        }

        public async Task<bool> SendResetPasswordCallback(string email, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new EntityNotFoundException("User not found");

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException();

            var resetPasswordToken = cryptoService.Encrypt(await userManager.GeneratePasswordResetTokenAsync(user));
            newPassword = cryptoService.Encrypt(newPassword);

            string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}resetPassword/confirm?email={user.Email}&newPassword={newPassword}&token={resetPasswordToken}";

            return await emailSender.Send(EmailMessages.ResetPasswordEmail(email, user.UserName, callbackUrl));
        }
    }
}