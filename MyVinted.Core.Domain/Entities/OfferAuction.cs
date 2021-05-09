using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class OfferAuction
    {
        public string Id { get; protected set; } = Utils.Id();
        public decimal NewPrice { get; protected set; }
        public bool IsAccepted { get; protected set; }
        public string OfferId { get; protected set; }

        public virtual Offer Offer { get; protected set; }

        public static OfferAuction Create(decimal newPrice, string offerId) => new OfferAuction
        {
            NewPrice = newPrice,
            OfferId = offerId
        };

        public void Update(decimal? newPrice = null, bool isAccepted = false) => (NewPrice, IsAccepted) = (newPrice.HasValue ? newPrice.Value : NewPrice, isAccepted);
    }
}