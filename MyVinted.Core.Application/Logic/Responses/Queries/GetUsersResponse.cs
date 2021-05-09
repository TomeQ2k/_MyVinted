using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetUsersResponse : BaseResponse
    {
        public IEnumerable<UserListDto> Users { get; init; }

        public GetUsersResponse(Error error = null) : base(error) { }
    }
}