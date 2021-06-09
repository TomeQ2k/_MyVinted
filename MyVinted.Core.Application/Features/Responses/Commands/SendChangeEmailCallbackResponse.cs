using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record SendChangeEmailCallbackResponse : BaseResponse
    {
        public SendChangeEmailCallbackResponse(Error error = null) : base(error) { }
    }
}