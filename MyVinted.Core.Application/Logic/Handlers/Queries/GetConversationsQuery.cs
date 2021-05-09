using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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