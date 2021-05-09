using System;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.SignalR
{
    public class MessengerHub : HubClient
    {
        public MessengerHub(IConnectionManager connectionManager, HubNamesDictionary hubNamesDictionary)
            : base(connectionManager, hubNamesDictionary) { }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await connectionManager.CloseConnection(hubNamesDictionary[typeof(MessengerHub)]);

            await base.OnDisconnectedAsync(exception);
        }
    }
}