using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record DeleteOpinionResponse : BaseResponse
    {
        public DeleteOpinionResponse(Error error = null) : base(error) { }
    }
}