using System.Threading.Tasks;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class BalanceValidationService : BaseValidationService, IBalanceValidationService
    {
        public BalanceValidationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<bool> HasEnoughFunds(string userId, decimal amount)
            => (await unitOfWork.BalanceAccountRepository.Find(b => b.AccountId == userId))?.Balance - (decimal)amount / Constants.MoneyMultiplier >= 0;
    }
}