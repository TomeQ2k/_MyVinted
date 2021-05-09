using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IStripeTokenCleaner
    {
        Task<bool> ClearUnusedTokens();
    }
}