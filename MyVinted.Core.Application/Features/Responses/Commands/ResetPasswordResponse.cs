using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ResetPasswordResponse : BaseResponse
    {
        public ResetPasswordResponse(Error error = null) : base(error) { }
    }
}