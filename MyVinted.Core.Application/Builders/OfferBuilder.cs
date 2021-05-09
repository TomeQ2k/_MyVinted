using MyVinted.Core.Application.Builders.Interfaces;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders
{
    public class OfferBuilder : IOfferBuilder
    {
        private readonly Offer offer = new Offer();

        public IOfferBuilder SetTitle(string title)
        {
            offer.SetTitle(title);

            return this;
        }

        public IOfferBuilder SetPrice(decimal price)
        {
            offer.SetPrice(price);

            return this;
        }

        public IOfferBuilder SetDescription(string description)
        {
            offer.SetDescription(description);

            return this;
        }

        public IOfferBuilder AllowBidding(bool allowBidding = false)
        {
            offer.SetAllowBidding(allowBidding);

            return this;
        }

        public IOfferBuilder IsVerified()
        {
            offer.Verify();

            return this;
        }

        public Offer Build() => offer;
    }
}