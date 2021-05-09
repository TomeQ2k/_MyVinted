using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.API.AppConfigs
{
    public static class GoogleAuthenticationAppConfig
    {
        public static AuthenticationBuilder ConfigureGoogleAuthentication(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
            => authenticationBuilder.AddGoogle(options =>
               {
                   IConfigurationSection googleAuthSection = configuration.GetSection(AppSettingsKeys.GoogleAuthSection);

                   options.ClientId = googleAuthSection[AppSettingsKeys.ClientId];
                   options.ClientSecret = googleAuthSection[AppSettingsKeys.ClientSecret];
               });
    }
}