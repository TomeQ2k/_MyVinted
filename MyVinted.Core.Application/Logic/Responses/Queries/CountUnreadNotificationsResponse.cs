using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record CountUnreadNotificationsResponse : BaseResponse
    {
        public int UnreadNotificationsCount { get; init; }

        public CountUnreadNotificationsResponse(Error error = null) : base(error) { }
    }
}