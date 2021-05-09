using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using IdentityResult = MyVinted.Core.Application.Results.IdentityResult;

namespace MyVinted.Core.Application.Services
{
    public abstract class BaseExternalIdentityService : IExternalIdentityService
    {
        protected readonly UserManager<User> userManager;
        protected readonly IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator;
        protected readonly IRolesManager rolesManager;
        protected readonly IUnitOfWork unitOfWork;

        public BaseExternalIdentityService(UserManager<User> userManager, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IRolesManager rolesManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.jwtAuthorizationTokenGenerator = jwtAuthorizationTokenGenerator;
            this.rolesManager = rolesManager;
            this.unitOfWork = unitOfWork;
        }

        public abstract Task<IdentityResult> SignInWithExternalProvider(string provider, string idToken);

        protected async Task<IdentityResult> AddUserLogin(string provider, string providerKey, string email, string username = null, string pictureUrl = null)
        {
            var userLoginInfo = new UserLoginInfo(provider, providerKey, provider);

            var user = await userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey)
                ?? await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = User.Create(email, username ?? email);
                await userManager.CreateAsync(user);
            }

            await userManager.AddLoginAsync(user, userLoginInfo);

            if (user == null)
                throw new ExternalAuthException("Invalid external authentication");

            await rolesManager.AdmitRole(RoleName.ExternalUser, user);

            if (!user.IsRegistered())
            {
                user.SetAvatarUrl(pictureUrl);
                await unitOfWork.Complete();
            }

            if (user.IsBlocked)
                throw new BlockException();

            var token = await jwtAuthorizationTokenGenerator.GenerateToken(user);

            return new IdentityResult(token, user);
        }
    }
}