using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetUserResponse : BaseResponse
    {
        public UserDto User { get; init; }

        public GetUserResponse(Error error = null) : base(error) { }
    }
}