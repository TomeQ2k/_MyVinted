using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record RemoveCartItemResponse : BaseResponse
    {
        public RemoveCartItemResponse(Error error = null) : base(error) { }
    }
}