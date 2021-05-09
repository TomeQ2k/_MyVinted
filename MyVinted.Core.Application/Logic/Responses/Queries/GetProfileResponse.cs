using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetProfileResponse : BaseResponse
    {
        public UserProfileDto UserProfile { get; init; }

        public GetProfileResponse(Error error = null) : base(error) { }
    }
}