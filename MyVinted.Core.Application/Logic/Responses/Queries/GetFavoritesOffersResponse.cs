using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetFavoritesOffersResponse : BaseResponse
    {
        public IEnumerable<OfferListDto> FavoritesOffers { get; init; }

        public GetFavoritesOffersResponse(Error error = null) : base(error) { }
    }
}