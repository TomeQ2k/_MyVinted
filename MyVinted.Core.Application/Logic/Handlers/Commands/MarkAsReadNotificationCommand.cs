using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class MarkAsReadNotificationCommand : IRequestHandler<MarkAsReadNotificationRequest, MarkAsReadNotificationResponse>
    {
        private readonly INotifier notifier;

        public MarkAsReadNotificationCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<MarkAsReadNotificationResponse> Handle(MarkAsReadNotificationRequest request, CancellationToken cancellationToken)
            => await notifier.MarkAsRead(request.NotificationId)
                ? new MarkAsReadNotificationResponse()
                : throw new CrudException("Marking notification as read failed");
    }
}