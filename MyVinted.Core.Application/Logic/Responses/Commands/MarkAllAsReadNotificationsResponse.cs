using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record MarkAllAsReadNotificationsResponse : BaseResponse
    {
        public MarkAllAsReadNotificationsResponse(Error error = null) : base(error) { }
    }
}