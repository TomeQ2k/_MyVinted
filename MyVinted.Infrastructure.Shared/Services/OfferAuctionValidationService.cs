using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OfferAuctionValidationService : BaseValidationService, IOfferAuctionValidationService
    {
        public OfferAuctionValidationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool ValidateNewPrice(decimal newPrice, decimal oldPrice)
            => newPrice < oldPrice && newPrice >= 1 ? true : throw new ServerException($"New price must be a value between: 1 and {oldPrice} $");

        public bool ValidateOwnerPermissions(string ownerId, string currentUserId)
            => ownerId == currentUserId ? true : throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);
    }
}