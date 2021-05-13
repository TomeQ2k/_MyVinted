using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Enums;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using IdentityResult = MyVinted.Core.Application.Results.IdentityResult;
using MyVinted.Infrastructure.Shared.Specifications;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator;
        private readonly ICryptoService cryptoService;
        private readonly IRolesManager rolesManager;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator,
            ICryptoService cryptoService, IRolesManager rolesManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtAuthorizationTokenGenerator = jwtAuthorizationTokenGenerator;
            this.cryptoService = cryptoService;
            this.rolesManager = rolesManager;
        }

        public async Task<IdentityResult> SignIn(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new InvalidCredentialsException("Invalid email address or password");

            if (UserIsExternalSpecification.Create().IsSatisfied(user))
                throw new InvalidCredentialsException("Invalid email address or password");

            if (!UserConfirmedSpecification.Create().IsSatisfied(user))
                throw new AccountNotConfirmedException("Account is not confirmed");

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException();

            return (await signInManager.CheckPasswordSignInAsync(user, password, false)).Succeeded
                ? new IdentityResult(await jwtAuthorizationTokenGenerator.GenerateToken(user), user)
                : throw new InvalidCredentialsException("Invalid email address or password");
        }

        public async Task<User> SignUp(string email, string username, string password)
        {
            var user = User.Create(email, username);

            if ((await userManager.CreateAsync(user, password)).Succeeded)
                return await rolesManager.AdmitRole(RoleName.User, user)
                    ? user : throw new AuthException("Admitting user role failed");

            throw new AuthException("Creating account failed");
        }

        public async Task<bool> ConfirmAccount(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new EntityNotFoundException("User not found");

            token = cryptoService.Decrypt(token);

            return (await userManager.ConfirmEmailAsync(user, token)).Succeeded;
        }

        public async Task<string> GenerateConfirmAccountToken(User user)
            => cryptoService.Encrypt(await userManager.GenerateEmailConfirmationTokenAsync(user));
    }
}