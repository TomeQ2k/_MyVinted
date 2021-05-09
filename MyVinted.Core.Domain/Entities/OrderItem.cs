using System;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class OrderItem
    {
        public string Id { get; protected set; } = Utils.Id();
        public long Amount { get; protected set; }
        public OrderType Type { get; protected set; }
        public string ProductName { get; protected set; }
        public string UserName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public string OrderId { get; protected set; }
        public string CartId { get; protected set; }
        public string OptionalData { get; protected set; }

        public virtual Order Order { get; protected set; }
        public virtual Cart Cart { get; protected set; }

        public void SetAmount(long amount) => Amount = amount;

        public void SetType(OrderType type) => Type = type;

        public void SetProductName(string productName) => ProductName = productName;

        public void SetUserName(string userName) => UserName = userName;

        public void SetEmail(string email) => Email = email;

        public void SetOrderId(string orderId) => OrderId = orderId;

        public void SetCartId(string cartId) => CartId = cartId;

        public void SetOptionalData(string optionalData) => OptionalData = optionalData;
    }
}