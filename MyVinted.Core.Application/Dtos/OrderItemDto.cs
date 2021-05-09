using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Application.Dtos
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public long Amount { get; set; }
        public OrderType Type { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
        public string CartId { get; set; }
        public string OptionalData { get; set; }
    }
}