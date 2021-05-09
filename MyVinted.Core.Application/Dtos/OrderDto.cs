using System;
using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public long TotalAmount { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }
}