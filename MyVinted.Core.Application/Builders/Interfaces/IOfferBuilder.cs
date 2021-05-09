using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders.Interfaces
{
    public interface IOfferBuilder : IBuilder<Offer>
    {
        IOfferBuilder SetTitle(string title);
        IOfferBuilder SetPrice(decimal price);
        IOfferBuilder SetDescription(string description);
        IOfferBuilder AllowBidding(bool allowBidding = false);
        IOfferBuilder IsVerified();
    }
}