using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class BookingCleaner : IBookingCleaner
    {
        private readonly IUnitOfWork unitOfWork;

        public BookingCleaner(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ClearExpiredBookings()
        {
            var expiredBookedOffers = await unitOfWork.OfferRepository.GetExpiredBookedOffers();

            var expiredCartItems = await unitOfWork.OrderItemRepository.GetWhere(i => i.DateCreated.AddMinutes(Constants.BookingHostedServiceTimeInMinutes) < DateTime.Now
                && i.OrderId == null);

            var emptyCarts = await unitOfWork.CartRepository.GetWhere(c => c.Items.Count == 0);

            expiredBookedOffers.ToList().ForEach(o => o.SetBookingUserId(null));

            unitOfWork.OfferRepository.UpdateRange(expiredBookedOffers);
            unitOfWork.OrderItemRepository.DeleteRange(expiredCartItems);
            unitOfWork.CartRepository.DeleteRange(emptyCarts);

            return await unitOfWork.Complete();
        }
    }
}