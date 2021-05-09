using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record CountUnreadMessagesResponse : BaseResponse
    {
        public int UnreadMessagesCount { get; init; }

        public CountUnreadMessagesResponse(Error error = null) : base(error) { }
    }
}