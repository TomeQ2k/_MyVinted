using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record DeleteAvatarResponse : BaseResponse
    {
        public DeleteAvatarResponse(Error error = null) : base(error) { }
    }
}