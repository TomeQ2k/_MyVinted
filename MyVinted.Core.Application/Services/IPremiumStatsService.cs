using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IPremiumStatsService : IStatsService
    {
        Task<int> CountOfferLikes();
        Task<int> CountUserFollows();
        Task<int> CountOpinions();
    }
}