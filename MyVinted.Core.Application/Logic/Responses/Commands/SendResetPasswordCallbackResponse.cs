using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record SendResetPasswordCallbackResponse : BaseResponse
    {
        public SendResetPasswordCallbackResponse(Error error = null) : base(error) { }
    }
}