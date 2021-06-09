using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record DeleteOfferPhotoResponse : BaseResponse
    {
        public DeleteOfferPhotoResponse(Error error = null) : base(error) { }
    }
}