using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IAdminStatsService : IStatsService
    {
        Task<int> CountAccounts();
        Task<double> AverageOffersCountPerUser();
        Task<double> AverageUserOpinion();
    }
}