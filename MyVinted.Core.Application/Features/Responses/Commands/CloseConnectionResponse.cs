using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record CloseConnectionResponse : BaseResponse
    {
        public CloseConnectionResponse(Error error = null) : base(error) { }
    }
}