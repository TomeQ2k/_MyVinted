using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Commands
{
    public record ChangeEmailResponse : BaseResponse
    {
        public ChangeEmailResponse(Error error = null) : base(error) { }
    }
}