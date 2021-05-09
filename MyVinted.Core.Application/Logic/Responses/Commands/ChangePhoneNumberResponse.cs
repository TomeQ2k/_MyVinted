using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record ChangePhoneNumberResponse : BaseResponse
    {
        public ChangePhoneNumberResponse(Error error = null) : base(error) { }
    }
}