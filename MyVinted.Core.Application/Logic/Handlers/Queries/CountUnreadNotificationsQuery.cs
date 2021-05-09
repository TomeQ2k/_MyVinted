using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
{
    public class CountUnreadNotificationsQuery : IRequestHandler<CountUnreadNotificationsRequest, CountUnreadNotificationsResponse>
    {
        private readonly IReadOnlyNotifier notifier;

        public CountUnreadNotificationsQuery(IReadOnlyNotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<CountUnreadNotificationsResponse> Handle(CountUnreadNotificationsRequest request, CancellationToken cancellationToken)
            => new CountUnreadNotificationsResponse { UnreadNotificationsCount = await notifier.CountUnreadNotifications() };
    }
}