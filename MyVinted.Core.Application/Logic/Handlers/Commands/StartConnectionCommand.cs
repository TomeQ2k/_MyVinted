using MediatR;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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