using Microsoft.Extensions.DependencyInjection;
using MyVinted.API.BackgroundServices;

namespace MyVinted.API.AppConfigs
{
    public static class ServerHostedServicesAppConfig
    {
        public static IServiceCollection ConfigureServerHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<BookingHostedService>();
            services.AddHostedService<StripeTokenHostedService>();

            return services;
        }
    }
}