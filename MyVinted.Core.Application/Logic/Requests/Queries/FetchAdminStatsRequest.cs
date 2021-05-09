using MediatR;
using MyVinted.Core.Application.Logic.Responses.Queries;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public record FetchAdminStatsRequest : IRequest<FetchAdminStatsResponse>
    { }
}