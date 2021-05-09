using MyVinted.Core.Application.Builders.Interfaces;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders
{
    public class OrderItemBuilder : IOrderItemBuilder
    {
        private readonly OrderItem orderItem = new OrderItem();

        public IOrderItemBuilder SetAmount(decimal amount)
        {
            orderItem.SetAmount((long)(amount * Constants.MoneyMultiplier));

            return this;
        }

        public IOrderItemBuilder SetType(OrderType type)
        {
            orderItem.SetType(type);

            return this;
        }

        public IOrderItemBuilder SetProductName(string productName)
        {
            orderItem.SetProductName(productName);

            return this;
        }

        public IOrderItemBuilder SetUserName(string userName)
        {
            orderItem.SetUserName(userName);

            return this;
        }

        public IOrderItemBuilder SetEmail(string email)
        {
            orderItem.SetEmail(email);

            return this;
        }

        public IOrderItemBuilder SetOrderId(string orderId)
        {
            orderItem.SetOrderId(orderId);

            return this;
        }

        public IOrderItemBuilder SetCartId(string cartId)
        {
            orderItem.SetCartId(cartId);

            return this;
        }

        public IOrderItemBuilder SetOptionalData(string optionalData)
        {
            orderItem.SetOptionalData(optionalData);

            return this;
        }

        public OrderItem Build() => orderItem;
    }
}