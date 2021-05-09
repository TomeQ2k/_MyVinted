using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextReader httpContextReader;

        public BookingService(IUnitOfWork unitOfWork, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextReader = httpContextReader;
        }

        public async Task<bool> BookOffer(string offerId)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthException(ErrorMessages.NotAuthenticatedMessage);
            var offer = await unitOfWork.OfferRepository.Get(offerId) ??
                        throw new EntityNotFoundException("Offer not found");

            if (offer.BookingUserId != null)
                throw new DuplicateException("Offer is already booked");

            offer.SetBookingUserId(currentUserId);

            return await unitOfWork.Complete();
        }

        public async Task<bool> CancelBooking(Cart cart)
        {
            var bookedOffer = cart.User.BookedOffers.FirstOrDefault(o => o.BookingUserId == cart.User.Id);
            bookedOffer?.SetBookingUserId(null);

            unitOfWork.OfferRepository.Update(bookedOffer);

            return await unitOfWork.Complete();
        }
    }
}