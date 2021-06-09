using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
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