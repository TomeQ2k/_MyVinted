using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class CountUnreadMessagesQuery : IRequestHandler<CountUnreadMessagesRequest, CountUnreadMessagesResponse>
    {
        private readonly IReadOnlyMessenger messenger;

        public CountUnreadMessagesQuery(IReadOnlyMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<CountUnreadMessagesResponse> Handle(CountUnreadMessagesRequest request, CancellationToken cancellationToken)
            => new CountUnreadMessagesResponse { UnreadMessagesCount = await messenger.CountUnreadMessages() };
    }
}