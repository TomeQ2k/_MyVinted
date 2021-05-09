using Microsoft.AspNetCore.SignalR;
using MyVinted.Core.Application.SignalR;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.SignalR
{
    public class HubManager<THub> : IHubManager<THub> where THub : HubClient
    {
        private readonly IHubContext<THub> hubContext;
        private readonly IConnectionManager connectionManager;
        private readonly HubNamesDictionary hubNamesDictionary;

        public HubManager(IHubContext<THub> hubContext, IConnectionManager connectionManager, HubNamesDictionary hubNamesDictionary)
        {
            this.hubContext = hubContext;
            this.connectionManager = connectionManager;
            this.hubNamesDictionary = hubNamesDictionary;
        }

        public async Task Invoke(string actionName, string clientId, params object[] values)
        {
            string connectionId = await connectionManager.GetConnectionId(clientId, hubNamesDictionary[typeof(THub)]);

            if (!string.IsNullOrEmpty(connectionId))
                await hubContext.Clients.Client(await connectionManager.GetConnectionId(clientId, hubNamesDictionary[typeof(THub)])).SendAsync(actionName, values);
        }

        public async Task InvokeToAll(string actionName, params object[] values)
            => await hubContext.Clients.All.SendAsync(actionName, values);
    }
}