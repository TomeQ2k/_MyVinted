using System;
using System.Collections.Generic;
using System.Linq;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Offer
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public string Description { get; protected set; }
        public bool AllowBidding { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateUpdated { get; protected set; } = DateTime.Now;
        public bool IsVerified { get; protected set; }
        public bool IsBought { get; protected set; }
        public string CategoryId { get; protected set; }
        public string OwnerId { get; protected set; }
        public string BookingUserId { get; protected set; }

        public virtual Category Category { get; protected set; }
        public virtual User Owner { get; protected set; }
        public virtual OfferAuction OfferAuction { get; protected set; }
        public virtual User BookingUser { get; protected set; }

        public virtual ICollection<OfferPhoto> OfferPhotos { get; protected set; } = new HashSet<OfferPhoto>();
        public virtual ICollection<OfferLike> OfferLikes { get; protected set; } = new HashSet<OfferLike>();

        public void SetTitle(string title) => Title = title;

        public void SetPrice(decimal price) => Price = price;

        public void SetDescription(string description) => Description = description;

        public void SetAllowBidding(bool allowBidding) => AllowBidding = allowBidding;

        public void UpdateDate() => DateUpdated = DateTime.Now;

        public string GetFirstPhotoUrl() =>
            StorageLocation.BuildLocation(OfferPhotos.Select(p => p.Path).FirstOrDefault());

        public void SetBookingUserId(string userId) => BookingUserId = userId;

        public void Verify() => IsVerified = true;

        public void BuyOffer()
        {
            IsBought = true;
            BookingUserId = null;
        }
    }
}