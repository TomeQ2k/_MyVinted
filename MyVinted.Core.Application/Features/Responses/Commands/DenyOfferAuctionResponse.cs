using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record DenyOfferAuctionResponse : BaseResponse
    {
        public DenyOfferAuctionResponse(Error error = null) : base(error) { }
    }
}