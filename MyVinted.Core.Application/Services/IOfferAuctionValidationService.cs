namespace MyVinted.Core.Application.Services
{
    public interface IOfferAuctionValidationService
    {
        bool ValidateNewPrice(decimal newPrice, decimal oldPrice);
        bool ValidateOwnerPermissions(string ownerId, string currentUserId);
    }
}