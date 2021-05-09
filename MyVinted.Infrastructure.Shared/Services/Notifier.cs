using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class Notifier : INotifier
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly INotifierValidationService notifierValidationService;
        private readonly IHttpContextReader httpContextReader;

        public Notifier(IUnitOfWork unitOfWork, INotifierValidationService notifierValidationService, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.notifierValidationService = notifierValidationService;
            this.httpContextReader = httpContextReader;
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
            => await unitOfWork.NotificationRepository.GetOrderedNotifications(httpContextReader.CurrentUserId);

        public async Task<Notification> Push(string text, string userId)
        {
            var notification = Notification.Create(text, userId);

            unitOfWork.NotificationRepository.Add(notification);

            return await unitOfWork.Complete() ? notification : null;
        }

        public async Task<bool> MarkAsRead(string notificationId)
        {
            var notification = await unitOfWork.NotificationRepository.Get(notificationId)
                               ?? throw new EntityNotFoundException("Notification not found");

            if (!notifierValidationService.ValidateUserPermissions(notification))
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            notification.MarkAsRead();

            unitOfWork.NotificationRepository.Update(notification);

            return await unitOfWork.Complete();
        }

        public async Task<bool> MarkAllAsRead()
        {
            var notifications = await unitOfWork.NotificationRepository.GetWhere(n => n.UserId == httpContextReader.CurrentUserId);

            notifications.ToList().ForEach(n => n.MarkAsRead());

            unitOfWork.NotificationRepository.UpdateRange(notifications);

            return await unitOfWork.Complete();
        }

        public async Task<bool> RemoveNotification(string notificationId)
        {
            var notification = await unitOfWork.NotificationRepository.Get(notificationId)
                               ?? throw new EntityNotFoundException("Notification not found");

            if (!notifierValidationService.ValidateUserPermissions(notification))
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            unitOfWork.NotificationRepository.Delete(notification);

            return await unitOfWork.Complete();
        }

        public async Task<bool> ClearAllNotifications()
        {
            var notifications = await unitOfWork.NotificationRepository.GetWhere(n => n.UserId == httpContextReader.CurrentUserId);

            unitOfWork.NotificationRepository.DeleteRange(notifications);

            return await unitOfWork.Complete();
        }

        public async Task<int> CountUnreadNotifications()
            => await unitOfWork.NotificationRepository.CountUnreadNotifications(httpContextReader.CurrentUserId);
    }
}