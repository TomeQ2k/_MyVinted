using System;
using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class OrderAdminDto
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public long TotalAmount { get; set; }
        public bool IsValidated { get; set; }
        public string UserId { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }
}