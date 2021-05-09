using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record FetchAdminStatsResponse : BaseResponse
    {
        public AdminStatsModel StatsModel { get; init; }

        public FetchAdminStatsResponse(Error error = null) : base(error) { }
    }
}