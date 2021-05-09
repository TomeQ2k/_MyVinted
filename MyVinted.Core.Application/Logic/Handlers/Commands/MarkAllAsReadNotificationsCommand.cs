using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class MarkAllAsReadNotificationsCommand : IRequestHandler<MarkAllAsReadNotificationsRequest, MarkAllAsReadNotificationsResponse>
    {
        private readonly INotifier notifier;

        public MarkAllAsReadNotificationsCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<MarkAllAsReadNotificationsResponse> Handle(MarkAllAsReadNotificationsRequest request, CancellationToken cancellationToken)
            => await notifier.MarkAllAsRead()
                ? new MarkAllAsReadNotificationsResponse()
                : throw new CrudException("Marking all notifications as read failed");
    }
}