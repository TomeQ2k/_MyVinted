using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.SignalR
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextReader httpContextReader;

        public ConnectionManager(IUnitOfWork unitOfWork, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextReader = httpContextReader;
        }

        public async Task<bool> StartConnection(string connectionId, string hubName)
        {
            var connectionsToDelete = await unitOfWork.ConnectionRepository.GetWhere(c =>
                c.UserId == httpContextReader.CurrentUserId && c.HubName.ToUpper() == hubName.ToUpper());

            unitOfWork.ConnectionRepository.DeleteRange(connectionsToDelete);

            var connection = Connection.Create(httpContextReader.CurrentUserId, connectionId, hubName);

            unitOfWork.ConnectionRepository.Add(connection);

            return await unitOfWork.Complete();
        }

        public async Task<bool> CloseConnection(string hubName)
        {
            var connectionsToDelete = await unitOfWork.ConnectionRepository.GetWhere(c =>
                c.UserId == httpContextReader.CurrentUserId && c.HubName.ToUpper() == hubName.ToUpper());

            if (connectionsToDelete.Any())
            {
                unitOfWork.ConnectionRepository.DeleteRange(connectionsToDelete);

                return await unitOfWork.Complete();
            }

            return true;
        }

        public async Task<string> GetConnectionId(string userId, string hubName)
            => (await unitOfWork.ConnectionRepository
                .Find(c => c.UserId == userId && c.HubName.ToUpper() == hubName.ToUpper()))?.ConnectionId;
    }
}