using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetCartResponse : BaseResponse
    {
        public CartDto Cart { get; init; }

        public GetCartResponse(Error error = null) : base(error) { }
    }
}