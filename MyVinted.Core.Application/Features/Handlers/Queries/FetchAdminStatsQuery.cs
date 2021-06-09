using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class FetchAdminStatsQuery : IRequestHandler<FetchAdminStatsRequest, FetchAdminStatsResponse>
    {
        private readonly IAdminStatsService statsService;

        public FetchAdminStatsQuery(IAdminStatsService statsService)
        {
            this.statsService = statsService;
        }

        public async Task<FetchAdminStatsResponse> Handle(FetchAdminStatsRequest request, CancellationToken cancellationToken)
            => new FetchAdminStatsResponse
            {
                StatsModel = new AdminStatsModel
                (
                    await statsService.CountOffers(),
                    await statsService.CountOrders(),
                    await statsService.SumOrdersTotalAmount(),
                    await statsService.CountAccounts(),
                    await statsService.AverageOffersCountPerUser(),
                    await statsService.AverageUserOpinion()
                )
            };
    }
}