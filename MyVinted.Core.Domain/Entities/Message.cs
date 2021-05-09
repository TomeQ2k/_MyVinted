using System;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Message
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Text { get; protected set; }
        public DateTime DateSent { get; protected set; } = DateTime.Now;
        public string SenderId { get; protected set; }
        public string RecipientId { get; protected set; }
        public bool IsRead { get; protected set; }
        public bool IsLiked { get; protected set; }

        public virtual User Sender { get; protected set; }
        public virtual User Recipient { get; protected set; }

        public static Message Create(string text) => new Message { Text = text };

        public void MarkAsRead() => IsRead = true;

        public void ToggleIsLiked() => IsLiked = !IsLiked;
    }
}