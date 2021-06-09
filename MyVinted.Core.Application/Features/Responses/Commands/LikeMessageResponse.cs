using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record LikeMessageResponse : BaseResponse
    {
        public LikeMessageResponse(Error error = null) : base(error) { }
    }
}