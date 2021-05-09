using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
{
    public class FetchPremiumStatsQuery : IRequestHandler<FetchPremiumStatsRequest, FetchPremiumStatsResponse>
    {
        private readonly IPremiumStatsService statsService;

        public FetchPremiumStatsQuery(IPremiumStatsService statsService)
        {
            this.statsService = statsService;
        }

        public async Task<FetchPremiumStatsResponse> Handle(FetchPremiumStatsRequest request, CancellationToken cancellationToken)
            => new FetchPremiumStatsResponse
            {
                StatsModel = new PremiumStatsModel
                (
                    await statsService.CountOffers(),
                    await statsService.CountOrders(),
                    await statsService.SumOrdersTotalAmount(),
                    await statsService.CountOfferLikes(),
                    await statsService.CountUserFollows(),
                    await statsService.CountOpinions()
                )
            };
    }
}