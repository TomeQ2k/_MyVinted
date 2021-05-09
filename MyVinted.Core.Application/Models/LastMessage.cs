using System;

namespace MyVinted.Core.Application.Models
{
    public record LastMessage
    (
        string SenderId,
        string SenderName,
        string Text,
        DateTime DateSent,
        bool IsRead,
        bool IsCurrentUserSender
    );
}