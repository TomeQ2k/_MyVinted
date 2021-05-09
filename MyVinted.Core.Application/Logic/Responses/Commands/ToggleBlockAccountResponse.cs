using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record ToggleBlockAccountResponse : BaseResponse
    {
        public bool IsBlocked { get; init; }

        public ToggleBlockAccountResponse(Error error = null) : base(error)
        {
        }
    }
}