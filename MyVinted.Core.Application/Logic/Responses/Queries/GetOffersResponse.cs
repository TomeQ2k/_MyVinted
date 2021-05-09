using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetOffersResponse : BaseResponse
    {
        public IEnumerable<OfferListDto> Offers { get; init; }

        public GetOffersResponse(Error error = null) : base(error) { }
    }
}