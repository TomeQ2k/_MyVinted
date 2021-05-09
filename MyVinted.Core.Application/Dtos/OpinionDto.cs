using System;

namespace MyVinted.Core.Application.Dtos
{
    public class OpinionDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public string CreatorId { get; set; }
        public string CreatorUserName { get; set; }
    }
}