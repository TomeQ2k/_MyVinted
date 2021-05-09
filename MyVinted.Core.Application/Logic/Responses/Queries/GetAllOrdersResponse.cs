using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetAllOrdersResponse : BaseResponse
    {
        public IEnumerable<OrderAdminDto> Orders { get; init; }

        public GetAllOrdersResponse(Error error = null) : base(error)
        {
        }
    }
}