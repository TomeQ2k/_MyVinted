using MediatR;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class CloseConnectionCommand : IRequestHandler<CloseConnectionRequest, CloseConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;

        public CloseConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public async Task<CloseConnectionResponse> Handle(CloseConnectionRequest request, CancellationToken cancellationToken)
        {
            await connectionManager.CloseConnection(request.HubName);

            return new CloseConnectionResponse();
        }
    }
}