using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface INotifierValidationService
    {
        bool ValidateUserPermissions(Notification notification);
    }
}