using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record ChangePasswordResponse : BaseResponse
    {
        public ChangePasswordResponse(Error error = null) : base(error) { }
    }
}