using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Queries
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