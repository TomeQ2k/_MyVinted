using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record UpdateOfferResponse : BaseResponse
    {
        public UpdateOfferResponse(Error error = null) : base(error) { }
    }
}