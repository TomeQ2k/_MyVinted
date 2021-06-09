using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record CountUnreadNotificationsResponse : BaseResponse
    {
        public int UnreadNotificationsCount { get; init; }

        public CountUnreadNotificationsResponse(Error error = null) : base(error) { }
    }
}