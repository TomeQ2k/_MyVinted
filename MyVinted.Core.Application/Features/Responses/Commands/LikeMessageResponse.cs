using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record LikeMessageResponse : BaseResponse
    {
        public bool IsLiked { get; init; }

        public LikeMessageResponse(Error error = null) : base(error)
        {
        }
    }
}