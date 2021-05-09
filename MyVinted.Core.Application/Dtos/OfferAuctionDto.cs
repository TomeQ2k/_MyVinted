namespace MyVinted.Core.Application.Dtos
{
    public class OfferAuctionDto
    {
        public string Id { get; set; }
        public decimal NewPrice { get; set; }
        public bool IsAccepted { get; set; }
        public string OfferId { get; set; }
    }
}