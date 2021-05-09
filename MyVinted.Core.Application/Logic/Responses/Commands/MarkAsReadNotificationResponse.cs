using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record MarkAsReadNotificationResponse : BaseResponse
    {
        public MarkAsReadNotificationResponse(Error error = null) : base(error) { }
    }
}