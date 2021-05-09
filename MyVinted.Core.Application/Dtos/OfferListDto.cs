using System;
using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class OfferListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsVerified { get; set; }
        public bool IsBought { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerUserName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string FirstPhotoUrl { get; set; }
        public int LikesCount { get; set; }
        public bool IsBooked { get; set; }

        public ICollection<OfferLikeDto> OfferLikes { get; set; }
    }
}