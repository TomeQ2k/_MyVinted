using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record StartConnectionResponse : BaseResponse
    {
        public StartConnectionResponse(Error error = null) : base(error) { }
    }
}