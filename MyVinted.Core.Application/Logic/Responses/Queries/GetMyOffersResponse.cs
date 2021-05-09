using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetMyOffersResponse : BaseResponse
    {
        public IEnumerable<OfferListDto> Offers { get; init; }

        public GetMyOffersResponse(Error error = null) : base(error) { }
    }
}