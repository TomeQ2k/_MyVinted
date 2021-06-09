using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.SignalR;

namespace MyVinted.Core.Application.Features.Handlers.Commands
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