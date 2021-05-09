using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyNotifier
    {
        Task<IEnumerable<Notification>> GetNotifications();

        Task<int> CountUnreadNotifications();
    }
}