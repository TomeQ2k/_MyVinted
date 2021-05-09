using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record SetAvatarResponse : BaseResponse
    {
        public string AvatarUrl { get; init; }

        public SetAvatarResponse(Error error = null) : base(error) { }
    }
}