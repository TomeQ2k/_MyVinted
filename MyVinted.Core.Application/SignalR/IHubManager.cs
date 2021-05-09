using System.Threading.Tasks;

namespace MyVinted.Core.Application.SignalR
{
    public interface IHubManager<THub> where THub : HubClient
    {
        Task Invoke(string actionName, string clientId, params object[] values);
        Task InvokeToAll(string actionName, params object[] values);
    }
}