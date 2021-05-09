using System.Threading.Tasks;

namespace MyVinted.Core.Application.SignalR
{
    public interface IConnectionManager
    {
        Task<bool> StartConnection(string connectionId, string hubName);
        Task<bool> CloseConnection(string hubName);

        Task<string> GetConnectionId(string userId, string hubName);
    }
}