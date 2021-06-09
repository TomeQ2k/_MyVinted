using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ClearAllNotificationsCommand : IRequestHandler<ClearAllNotificationsRequest, ClearAllNotificationsResponse>
    {
        private readonly INotifier notifier;

        public ClearAllNotificationsCommand(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public async Task<ClearAllNotificationsResponse> Handle(ClearAllNotificationsRequest request, CancellationToken cancellationToken)
            => await notifier.ClearAllNotifications()
                ? new ClearAllNotificationsResponse()
                : throw new CrudException("Clearing all notifications failed");
    }
}