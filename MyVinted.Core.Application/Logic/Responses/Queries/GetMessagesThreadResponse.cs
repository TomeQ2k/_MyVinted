using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record GetMessagesThreadResponse : BaseResponse
    {
        public IEnumerable<MessageDto> Messages { get; init; }

        public RecipientDto Recipient { get; init; }

        public GetMessagesThreadResponse(Error error = null) : base(error) { }
    }
}