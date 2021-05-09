using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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