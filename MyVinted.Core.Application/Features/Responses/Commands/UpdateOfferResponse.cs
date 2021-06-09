using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record UpdateOfferResponse : BaseResponse
    {
        public UpdateOfferResponse(Error error = null) : base(error) { }
    }
}