using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record StartConnectionResponse : BaseResponse
    {
        public StartConnectionResponse(Error error = null) : base(error) { }
    }
}