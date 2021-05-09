using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface INotifier : IReadOnlyNotifier
    {
        Task<Notification> Push(string text, string userId);

        Task<bool> MarkAsRead(string notificationId);
        Task<bool> MarkAllAsRead();

        Task<bool> RemoveNotification(string notificationId);
        Task<bool> ClearAllNotifications();
    }
}