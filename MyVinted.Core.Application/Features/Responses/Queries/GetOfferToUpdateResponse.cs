using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record GetOfferToUpdateResponse : BaseResponse
    {
        public OfferToUpdateDto OfferToUpdate { get; init; }

        public GetOfferToUpdateResponse(Error error = null) : base(error) { }
    }
}