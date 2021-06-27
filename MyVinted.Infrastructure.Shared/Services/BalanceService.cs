using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IUnitOfWork unitOfWork;

        public BalanceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BalanceAccount> CreateBalanceAccount(string accountId, decimal startBalance = Constants.DefaultAccountBalance)
        {
            var balanceAccountFromDatabase = await unitOfWork.BalanceAccountRepository.Find(b => b.AccountId == accountId);

            if (balanceAccountFromDatabase != null)
                return balanceAccountFromDatabase;

            var balanceAccount = BalanceAccount.Create(accountId, balance: startBalance);

            unitOfWork.BalanceAccountRepository.Add(balanceAccount);

            return await unitOfWork.Complete() ? balanceAccount : throw new ServerException("Creating balance account failed");
        }

        public async Task<decimal> AddBalance(string accountId, decimal balanceToAdd)
        {
            var balanceAccount = (await unitOfWork.UserRepository.FindById(accountId) ?? throw new EntityNotFoundException("Account not found")).BalanceAccount
                ?? await CreateBalanceAccount(accountId);

            if (!balanceAccount.AddBalance(balanceToAdd))
                throw new PaymentException("Insufficient funds to complete transaction");

            unitOfWork.BalanceAccountRepository.Update(balanceAccount);

            return await unitOfWork.Complete() ? balanceAccount.Balance : throw new ServerException("Adding balance to account failed");
        }
    }
}