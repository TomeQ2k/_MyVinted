using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ReadMessageResponse : BaseResponse
    {
        public ReadMessageResponse(Error error = null) : base(error) { }
    }
}