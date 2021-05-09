using MyVinted.Core.Common.Helpers;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IBalanceService
    {
        Task<BalanceAccount> CreateBalanceAccount(string accountId, decimal startBalance = Constants.DefaultAccountBalance);

        Task<decimal> AddBalance(string accountId, decimal balanceToAdd);
    }
}