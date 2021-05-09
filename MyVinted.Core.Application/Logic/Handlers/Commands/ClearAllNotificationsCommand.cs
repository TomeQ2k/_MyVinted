using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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