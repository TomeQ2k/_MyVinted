using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record DeleteOfferPhotoResponse : BaseResponse
    {
        public DeleteOfferPhotoResponse(Error error = null) : base(error) { }
    }
}