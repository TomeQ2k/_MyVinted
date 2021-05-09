using MyVinted.Core.Domain.Data;

namespace MyVinted.Core.Application.Services
{
    public abstract class BaseValidationService
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseValidationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}