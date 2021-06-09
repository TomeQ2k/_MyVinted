using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record GetOrdersResponse : BaseResponse
    {
        public IEnumerable<OrderAdminDto> Orders { get; init; }

        public GetOrdersResponse(Error error = null) : base(error)
        {
        }
    }
}