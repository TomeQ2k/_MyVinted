using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.API.BackgroundServices
{
    internal class StripeTokenHostedService : ServerHostedService
    {
        public StripeTokenHostedService(IServiceProvider services) : base(services)
        {
            TimeInterval = Constants.StripeTokenHostedServiceTimeInMinutes;
        }

        public override async void Callback(object state)
        {
            using (var scope = services.CreateScope())
            {
                var stripeTokenCleaner = scope.ServiceProvider.GetRequiredService<IStripeTokenCleaner>();

                await stripeTokenCleaner.ClearUnusedTokens();

                base.Callback(state);
            }
        }
    }
}