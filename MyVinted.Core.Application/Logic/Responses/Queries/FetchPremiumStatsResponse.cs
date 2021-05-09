using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record FetchPremiumStatsResponse : BaseResponse
    {
        public PremiumStatsModel StatsModel { get; init; }

        public FetchPremiumStatsResponse(Error error = null) : base(error) { }
    }
}