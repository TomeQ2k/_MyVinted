using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class OfferToUpdateDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool AllowBidding { get; set; }
        public string CategoryId { get; set; }
        public string OwnerId { get; set; }

        public ICollection<OfferPhotoDto> OfferPhotos { get; set; }
    }
}