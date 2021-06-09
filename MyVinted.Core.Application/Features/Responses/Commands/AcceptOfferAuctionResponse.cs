using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record AcceptOfferAuctionResponse : BaseResponse
    {
        public AcceptOfferAuctionResponse(Error error = null) : base(error) { }
    }
}