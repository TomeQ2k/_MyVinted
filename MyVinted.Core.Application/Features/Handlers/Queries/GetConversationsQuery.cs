using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetConversationsQuery : IRequestHandler<GetConversationsRequest, GetConversationsResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        private readonly IHttpContextWriter httpContextWriter;

        public GetConversationsQuery(IReadOnlyMessenger messenger, IHttpContextWriter httpContextWriter)
        {
            this.messenger = messenger;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetConversationsResponse> Handle(GetConversationsRequest request,
            CancellationToken cancellationToken)
        {
            var conversations = await messenger.GetConversations(request);

            httpContextWriter.AddPagination(conversations.CurrentPage, conversations.PageSize, conversations.TotalCount,
                conversations.TotalPages);

            return new GetConversationsResponse {Conversations = conversations};
        }
    }
}