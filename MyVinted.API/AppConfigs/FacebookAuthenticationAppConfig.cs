using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.API.AppConfigs
{
    public static class FacebookAuthenticationAppConfig
    {
        public static AuthenticationBuilder ConfigureFacebookAuthentication(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
            => authenticationBuilder.AddFacebook(options =>
            {
                IConfigurationSection facebookAuthSection = configuration.GetSection(AppSettingsKeys.FacebookAuthSection);

                options.AppId = facebookAuthSection[AppSettingsKeys.AppId];
                options.ClientSecret = facebookAuthSection[AppSettingsKeys.AppSecret];
            });
    }
}