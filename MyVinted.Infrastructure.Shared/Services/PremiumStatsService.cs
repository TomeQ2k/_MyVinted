using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Linq;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class PremiumStatsService : IPremiumStatsService
    {
        private readonly IReadOnlyAccountManager accountManager;

        public PremiumStatsService(IReadOnlyAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<int> CountOffers()
            => (await accountManager.GetCurrentUser()).Offers.Count;

        public async Task<int> CountOrders()
            => (await accountManager.GetCurrentUser()).Orders.Count;

        public async Task<long> SumOrdersTotalAmount()
            => (await accountManager.GetCurrentUser()).Orders.Sum(o => o.TotalAmount);

        public async Task<int> CountOfferLikes()
            => (await accountManager.GetCurrentUser()).Offers.Select(o => o.OfferLikes).Count();

        public async Task<int> CountUserFollows()
            => (await accountManager.GetCurrentUser()).Followings.Count;

        public async Task<int> CountOpinions()
            => (await accountManager.GetCurrentUser()).Opinions.Count;
    }
}