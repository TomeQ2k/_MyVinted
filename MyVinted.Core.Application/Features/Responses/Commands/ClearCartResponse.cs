using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ClearCartResponse : BaseResponse
    {
        public ClearCartResponse(Error error = null) : base(error) { }
    }
}