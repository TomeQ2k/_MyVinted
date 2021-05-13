using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class NotifierValidationService : BaseValidationService, INotifierValidationService
    {
        private readonly IHttpContextReader httpContextReader;

        public NotifierValidationService(IUnitOfWork unitOfWork, IHttpContextReader httpContextReader) : base(
            unitOfWork)
        {
            this.httpContextReader = httpContextReader;
        }

        public bool ValidateUserPermissions(Notification notification)
            => httpContextReader.CurrentUserId == notification?.UserId;
    }
}