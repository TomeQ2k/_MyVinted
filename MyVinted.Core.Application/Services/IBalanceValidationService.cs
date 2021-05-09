using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IBalanceValidationService
    {
        Task<bool> HasEnoughFunds(string userId, decimal amount);
    }
}