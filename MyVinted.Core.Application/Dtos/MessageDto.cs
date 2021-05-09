using System;

namespace MyVinted.Core.Application.Dtos
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateSent { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public bool IsRead { get; set; }
        public bool IsLiked { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatarUrl { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAvatarUrl { get; set; }
    }
}