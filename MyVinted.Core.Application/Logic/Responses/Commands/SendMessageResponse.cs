using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record SendMessageResponse : BaseResponse
    {
        public MessageDto Message { get; init; }

        public SendMessageResponse(Error error = null) : base(error) { }
    }
}