using System.Collections.Generic;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record GetConversationsResponse : BaseResponse
    {
        public IEnumerable<Conversation> Conversations { get; init; }

        public GetConversationsResponse(Error error = null) : base(error) { }
    }
}