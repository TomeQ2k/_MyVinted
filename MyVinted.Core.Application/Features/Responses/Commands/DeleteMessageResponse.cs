using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record DeleteMessageResponse : BaseResponse
    {
        public DeleteMessageResponse(Error error = null) : base(error) { }
    }
}