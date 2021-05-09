using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyMessenger
    {
        Task<PagedList<Conversation>> GetConversations(MessengerFiltersParams filters);
        Task<IPagedList<Message>> GetMessagesThread(string recipientId, MessengerFiltersParams filters);

        Task<int> CountUnreadMessages();
    }
}