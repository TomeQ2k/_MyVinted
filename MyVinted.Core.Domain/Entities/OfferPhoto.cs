namespace MyVinted.Core.Domain.Entities
{
    public class OfferPhoto : BaseFile
    {
        public string OfferId { get; protected set; }

        public virtual Offer Offer { get; protected set; }

        public static OfferPhoto Create(string path, string offerId) => new OfferPhoto
        {
            Path = path,
            OfferId = offerId
        };
    }
}