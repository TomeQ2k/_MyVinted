using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ClearAllNotificationsResponse : BaseResponse
    {
        public ClearAllNotificationsResponse(Error error = null) : base(error) { }
    }
}