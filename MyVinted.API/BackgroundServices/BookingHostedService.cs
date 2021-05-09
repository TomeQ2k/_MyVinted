using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.API.BackgroundServices
{
    internal class BookingHostedService : ServerHostedService
    {
        public BookingHostedService(IServiceProvider services) : base(services)
        {
            TimeInterval = Constants.BookingHostedServiceTimeInMinutes;
        }

        public override async void Callback(object state)
        {
            using (var scope = services.CreateScope())
            {
                var bookingCleaner = scope.ServiceProvider.GetRequiredService<IBookingCleaner>();

                await bookingCleaner.ClearExpiredBookings();

                base.Callback(state);
            }
        }
    }
}