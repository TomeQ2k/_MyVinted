using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.SignalR;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class StartConnectionCommand : IRequestHandler<StartConnectionRequest, StartConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;

        public StartConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public async Task<StartConnectionResponse> Handle(StartConnectionRequest request, CancellationToken cancellationToken)
        {
            await connectionManager.StartConnection(request.ConnectionId, request.HubName);

            return new StartConnectionResponse();
        }
    }
}