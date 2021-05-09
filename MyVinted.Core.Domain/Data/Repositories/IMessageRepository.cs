using System.Threading.Tasks;
using MyVinted.Core.Domain.Data.Models;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IPagedList<Message>> GetMessagesThread(string userId, string recipientId, (int PageNumber, int PageSize) pagination);

        Task<int> CountUnreadMessages(string userId);
    }
}