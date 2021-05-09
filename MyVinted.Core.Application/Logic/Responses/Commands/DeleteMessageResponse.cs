using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record DeleteMessageResponse : BaseResponse
    {
        public DeleteMessageResponse(Error error = null) : base(error) { }
    }
}