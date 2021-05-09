using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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