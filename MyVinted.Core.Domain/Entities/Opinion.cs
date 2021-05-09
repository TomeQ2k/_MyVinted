using System;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Opinion
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Text { get; protected set; }
        public int Rating { get; protected set; }
        public DateTime DateAdded { get; protected set; } = DateTime.Now;
        public string UserId { get; protected set; }
        public string CreatorId { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual User Creator { get; protected set; }

        public void SetText(string text) => Text = text;

        public void SetRating(int rating) => Rating = rating;

        public void AboutUser(string userId) => UserId = userId;

        public void AddedBy(string creatorId) => CreatorId = creatorId;
    }
}