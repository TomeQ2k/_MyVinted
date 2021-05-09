using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetUserOrdersResponse : BaseResponse
    {
        public IEnumerable<OrderDto> Orders { get; init; }

        public GetUserOrdersResponse(Error error = null) : base(error) { }
    }
}