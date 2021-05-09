using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record ChangeUsernameResponse : BaseResponse
    {
        public ChangeUsernameResponse(Error error = null) : base(error) { }
    }
}