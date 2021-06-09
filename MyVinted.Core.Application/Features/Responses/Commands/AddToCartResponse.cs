using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record AddToCartResponse : BaseResponse
    {
        public OrderItemDto CartItem { get; init; }

        public AddToCartResponse(Error error = null) : base(error) { }
    }
}