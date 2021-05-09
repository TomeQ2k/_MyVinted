using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders.Interfaces
{
    public interface IOrderItemBuilder : IBuilder<OrderItem>
    {
        IOrderItemBuilder SetAmount(decimal amount);
        IOrderItemBuilder SetType(OrderType type);
        IOrderItemBuilder SetProductName(string productName);
        IOrderItemBuilder SetUserName(string userName);
        IOrderItemBuilder SetEmail(string email);
        IOrderItemBuilder SetOrderId(string orderId);
        IOrderItemBuilder SetCartId(string cartId);
        IOrderItemBuilder SetOptionalData(string optionalData);
    }
}