using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using IdentityResult = MyVinted.Core.Application.Results.IdentityResult;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class GoogleIdentityService : BaseExternalIdentityService, IGoogleIdentityService
    {
        public IConfigurationSection GoogleAuthSection { get; }

        public GoogleIdentityService(UserManager<User> userManager, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IRolesManager rolesManager, IUnitOfWork unitOfWork, IConfiguration configuration)
            : base(userManager, jwtAuthorizationTokenGenerator, rolesManager, unitOfWork)
        {
            GoogleAuthSection = configuration.GetSection(AppSettingsKeys.GoogleAuthSection);
        }

        public override async Task<IdentityResult> SignInWithExternalProvider(string provider, string idToken)
        {
            var payload = await VerifyGoogleToken(idToken) ?? throw new ExternalAuthException("Invalid external authentication");

            return await AddUserLogin(provider, payload.Subject, provider,
                username: payload.Email, pictureUrl: payload.Picture);
        }

        public async Task<Payload> VerifyGoogleToken(string idToken)
        {
            try
            {
                var validationSettings = new ValidationSettings { Audience = new List<string> { GoogleAuthSection.GetValue<string>(AppSettingsKeys.ClientId) } };

                var payload = await ValidateAsync(idToken, validationSettings);

                return payload;
            }
            catch (Exception) { throw new ExternalAuthException("Google authentication token is invalid"); }
        }
    }
}