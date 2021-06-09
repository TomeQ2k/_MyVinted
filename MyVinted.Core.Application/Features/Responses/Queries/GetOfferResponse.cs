using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record GetOfferResponse : BaseResponse
    {
        public OfferDto Offer { get; init; }

        public GetOfferResponse(Error error = null) : base(error) { }
    }
}