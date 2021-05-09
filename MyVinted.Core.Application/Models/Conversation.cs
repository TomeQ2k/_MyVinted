namespace MyVinted.Core.Application.Models
{
    public record Conversation
    (
        LastMessage LastMessage,
        string RecipientId,
        string RecipientName,
        string RecipientAvatarUrl
    );
}