using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class StripeTokenCleaner : IStripeTokenCleaner
    {
        private readonly IUnitOfWork unitOfWork;

        public StripeTokenCleaner(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ClearUnusedTokens()
        {
            var unusedTokens = await unitOfWork.StripeTokenRepository.GetWhere(t => t.Order == null);

            unitOfWork.StripeTokenRepository.DeleteRange(unusedTokens);

            return await unitOfWork.Complete();
        }
    }
}