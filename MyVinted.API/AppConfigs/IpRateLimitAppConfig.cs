using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.API.AppConfigs
{
    public static class IpRateLimitAppConfig
    {
        public static IServiceCollection ConfigureIpRateLimit(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<IpRateLimitOptions>(configuration.GetSection(AppSettingsKeys.IpRateLimitingSection))
               .AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>()
               .AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>()
               .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}