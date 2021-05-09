using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OfferAuctionManager : IOfferAuctionManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextReader httpContextReader;
        private readonly IOfferAuctionValidationService offerAuctionValidationService;

        public OfferAuctionManager(IUnitOfWork unitOfWork, IHttpContextReader httpContextReader,
            IOfferAuctionValidationService offerAuctionValidationService)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextReader = httpContextReader;
            this.offerAuctionValidationService = offerAuctionValidationService;
        }

        public async Task<OfferAuction> CreateAuction(decimal newPrice, string offerId)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthException(ErrorMessages.NotAuthenticatedMessage);
            var offer = await unitOfWork.OfferRepository.Get(offerId) ??
                        throw new EntityNotFoundException("Offer not found");

            if (!offer.AllowBidding)
                throw new NoPermissionsException("You are not allowed to perform this action");

            if (offer.BookingUserId != null)
                throw new ServerException("Offer is currently booked");

            if (offer.Price == 1)
                throw new ServerException("Current price is minimum allowed price");

            if (currentUserId == offer.OwnerId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            offerAuctionValidationService.ValidateNewPrice(newPrice, offer.Price);

            if (offer.OfferAuction != null)
            {
                if (!offer.OfferAuction.IsAccepted)
                    throw new ServerException("Auction is currently waiting for offer owner response");

                offer.OfferAuction.Update(newPrice: newPrice);
                unitOfWork.OfferAuctionRepository.Update(offer.OfferAuction);

                return await unitOfWork.Complete()
                    ? offer.OfferAuction
                    : throw new ServerException("Updating offer auction failed");
            }

            var offerAuction = OfferAuction.Create(newPrice, offer.Id);
            unitOfWork.OfferAuctionRepository.Add(offerAuction);

            return await unitOfWork.Complete()
                ? offerAuction
                : throw new ServerException("Creating offer auction failed");
        }

        public async Task<bool> AcceptAuction(string auctionId, string offerId)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthException(ErrorMessages.NotAuthenticatedMessage);
            var offerAuction =
                await unitOfWork.OfferAuctionRepository.Find(a => a.Id == auctionId && a.OfferId == offerId)
                ?? throw new EntityNotFoundException("Auction not found");

            if (!offerAuction.Offer.AllowBidding)
                throw new NoPermissionsException("You are not allowed to perform this action");

            if (offerAuction.Offer.BookingUserId != null)
                throw new ServerException("Offer is currently booked");

            if (offerAuction.IsAccepted)
                throw new ServerException("Offer auction is already accepted");

            offerAuctionValidationService.ValidateOwnerPermissions(offerAuction.Offer.OwnerId, currentUserId);

            offerAuction.Update(isAccepted: true);
            offerAuction.Offer.SetPrice(offerAuction.NewPrice);

            unitOfWork.OfferAuctionRepository.Update(offerAuction);
            unitOfWork.OfferRepository.Update(offerAuction.Offer);

            return await unitOfWork.Complete();
        }

        public async Task<bool> DenyAuction(string auctionId, string offerId)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthException(ErrorMessages.NotAuthenticatedMessage);
            var offerAuction =
                await unitOfWork.OfferAuctionRepository.Find(a => a.Id == auctionId && a.OfferId == offerId)
                ?? throw new EntityNotFoundException("Auction not found");

            if (!offerAuction.Offer.AllowBidding)
                throw new NoPermissionsException("You are not allowed to perform this action");

            if (offerAuction.Offer.BookingUserId != null)
                throw new ServerException("Offer is currently booked");

            if (offerAuction.IsAccepted)
                throw new ServerException("Offer auction is already accepted");

            offerAuctionValidationService.ValidateOwnerPermissions(offerAuction.Offer.OwnerId, currentUserId);

            unitOfWork.OfferAuctionRepository.Delete(offerAuction);

            return await unitOfWork.Complete();
        }
    }
}