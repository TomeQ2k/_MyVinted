using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record LikeOfferResponse : BaseResponse
    {
        public bool IsLiked { get; init; }
        public OfferLikeDto Like { get; init; }

        public LikeOfferResponse(Error error = null) : base(error) { }
    }
}