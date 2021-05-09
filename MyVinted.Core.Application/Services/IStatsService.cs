using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IStatsService
    {
        Task<int> CountOffers();
        Task<int> CountOrders();
        Task<long> SumOrdersTotalAmount();
    }
}