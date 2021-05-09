using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyVinted.Core.Domain.Data.Repositories;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetOrderedNotifications(string userId)
            => await context.Notifications.Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();

        public async Task<int> CountUnreadNotifications(string userId)
            => await context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .CountAsync();
    }
}