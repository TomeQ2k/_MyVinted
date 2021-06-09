using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record FollowUserResponse : BaseResponse
    {
        public bool IsFollowed { get; init; }
        public UserFollowDto Follow { get; init; }

        public FollowUserResponse(Error error = null) : base(error) { }
    }
}