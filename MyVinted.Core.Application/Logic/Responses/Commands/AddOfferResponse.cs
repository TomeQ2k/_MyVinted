using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record AddOfferResponse : BaseResponse
    {
        public OfferDto Offer { get; init; }

        public AddOfferResponse(Error error = null) : base(error) { }
    }
}