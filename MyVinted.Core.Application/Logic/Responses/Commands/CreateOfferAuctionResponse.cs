using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record CreateOfferAuctionResponse : BaseResponse
    {
        public OfferAuctionDto OfferAuction { get; init; }

        public CreateOfferAuctionResponse(Error error = null) : base(error) { }
    }
}