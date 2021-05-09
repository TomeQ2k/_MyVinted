using System;

namespace MyVinted.Core.Application.Dtos
{
    public class NotificationDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
    }
}