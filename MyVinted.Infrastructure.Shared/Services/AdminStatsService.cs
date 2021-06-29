using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class AdminStatsService : IAdminStatsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminStatsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> CountOffers()
            => (await unitOfWork.OfferRepository.GetAll()).Count();

        public async Task<int> CountOrders()
            => (await unitOfWork.OrderRepository.GetAll()).Count();

        public async Task<long> SumOrdersTotalAmount()
            => (await unitOfWork.OrderRepository.GetAll()).Sum(o => o.TotalAmount);

        public async Task<int> CountAccounts()
            => (await unitOfWork.UserRepository.GetWhere(u => u.EmailConfirmed || string.IsNullOrEmpty(u.PasswordHash)))
                .Count();

        public async Task<double> AverageOffersCountPerUser()
        {
            int accountsCount = await CountAccounts();

            return accountsCount != 0 ? (double)(await CountOffers()) / accountsCount : 0;
        }

        public async Task<double> AverageUserOpinion()
        {
            int accountsCount = await CountAccounts();

            return accountsCount != 0 ? (double)(await unitOfWork.OpinionRepository.GetAll()).Sum(o => o.Rating) / accountsCount : 0;
        }
    }
}