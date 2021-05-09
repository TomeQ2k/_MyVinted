using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetOrderedNotifications(string userId);

        Task<int> CountUnreadNotifications(string userId);
    }
}