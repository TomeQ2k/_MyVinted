using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record PurchaseOrderResponse : BaseResponse, IJwtAuthorizationTokenResponse
    {
        public OrderDto Order { get; init; }

        public string Token { get; init; }

        public PurchaseOrderResponse(Error error = null) : base(error) { }
    }
}