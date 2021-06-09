using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class RemoveNotificationCommand : IRequestHandler<RemoveNotificationRequest, RemoveNotificationResponse>
    {
        private readonly INotifier notifier;

        public RemoveNotificationCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<RemoveNotificationResponse> Handle(RemoveNotificationRequest request, CancellationToken cancellationToken)
            => await notifier.RemoveNotification(request.NotificationId)
                ? new RemoveNotificationResponse()
                : throw new CrudException("Removing notification failed");
    }
}