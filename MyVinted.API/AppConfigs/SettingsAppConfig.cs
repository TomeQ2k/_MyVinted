using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Settings;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.API.AppConfigs
{
    public static class SettingsAppConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<StripeSettings>(configuration.GetSection(AppSettingsKeys.StripeSection));

            return services;
        }
    }
}