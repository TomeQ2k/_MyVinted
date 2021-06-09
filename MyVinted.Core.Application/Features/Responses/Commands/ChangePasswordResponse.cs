using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ChangePasswordResponse : BaseResponse
    {
        public ChangePasswordResponse(Error error = null) : base(error) { }
    }
}