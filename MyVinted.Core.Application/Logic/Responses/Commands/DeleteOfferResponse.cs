using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record DeleteOfferResponse : BaseResponse
    {
        public DeleteOfferResponse(Error error = null) : base(error) { }
    }
}