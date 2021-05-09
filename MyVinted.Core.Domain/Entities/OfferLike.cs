namespace MyVinted.Core.Domain.Entities
{
    public class OfferLike
    {
        public string OfferId { get; protected set; }
        public string UserId { get; protected set; }

        public virtual Offer Offer { get; protected set; }
        public virtual User User { get; protected set; }

        public static OfferLike Create(string offerId, string userId) => new OfferLike
        {
            OfferId = offerId,
            UserId = userId
        };
    }
}