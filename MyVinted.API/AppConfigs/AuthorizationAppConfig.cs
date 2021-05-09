using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.API.AppConfigs
{
    public static class AuthorizationAppConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
            => services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.PremiumPolicy, policy => policy.RequireRole(Constants.PremiumRole));
                options.AddPolicy(Constants.AdminPolicy, policy => policy.RequireRole(Constants.AdminRole));
            });
    }
}