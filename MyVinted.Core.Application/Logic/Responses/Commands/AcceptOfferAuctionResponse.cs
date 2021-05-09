using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record AcceptOfferAuctionResponse : BaseResponse
    {
        public AcceptOfferAuctionResponse(Error error = null) : base(error) { }
    }
}