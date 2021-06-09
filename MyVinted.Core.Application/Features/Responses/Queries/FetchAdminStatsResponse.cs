using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record FetchAdminStatsResponse : BaseResponse
    {
        public AdminStatsModel StatsModel { get; init; }

        public FetchAdminStatsResponse(Error error = null) : base(error) { }
    }
}