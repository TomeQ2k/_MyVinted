using System;
using System.Collections.Generic;
using System.Linq;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Order
    {
        public string Id { get; protected set; } = Utils.Id();
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public long TotalAmount { get; protected set; }
        public string UserId { get; protected set; }
        public string TokenId { get; protected set; }
        public bool IsValidated { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual StripeToken Token { get; protected set; }

        public virtual ICollection<OrderItem> Items { get; protected set; } = new HashSet<OrderItem>();

        public static Order Create(string userId, string tokenId) => new Order
        {
            UserId = userId,
            TokenId = tokenId
        };

        public void CalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);

        public void Validate() => IsValidated = true;
    }
}