using System;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Notification
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Text { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public bool IsRead { get; protected set; }
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }

        public static Notification Create(string text, string userId) => new Notification
        {
            Text = text,
            UserId = userId
        };

        public void MarkAsRead() => IsRead = true;
    }
}