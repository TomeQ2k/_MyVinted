using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record MarkAsReadNotificationResponse : BaseResponse
    {
        public MarkAsReadNotificationResponse(Error error = null) : base(error) { }
    }
}