using System.Collections.Generic;
using System.Linq;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Cart
    {
        public string Id { get; protected set; } = Utils.Id();
        public long TotalAmount { get; protected set; }
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual ICollection<OrderItem> Items { get; protected set; } = new HashSet<OrderItem>();

        public static Cart Create(string userId) => new Cart { UserId = userId };

        public void CalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);
    }
}