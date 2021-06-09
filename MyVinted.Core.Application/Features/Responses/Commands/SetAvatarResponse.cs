using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record SetAvatarResponse : BaseResponse
    {
        public string AvatarUrl { get; init; }

        public SetAvatarResponse(Error error = null) : base(error) { }
    }
}