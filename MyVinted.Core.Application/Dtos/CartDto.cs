using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class CartDto
    {
        public string Id { get; set; }
        public long TotalAmount { get; set; }
        public string UserId { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }
}