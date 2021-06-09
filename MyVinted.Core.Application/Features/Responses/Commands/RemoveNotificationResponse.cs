using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record RemoveNotificationResponse : BaseResponse
    {
        public RemoveNotificationResponse(Error error = null) : base(error) { }
    }
}