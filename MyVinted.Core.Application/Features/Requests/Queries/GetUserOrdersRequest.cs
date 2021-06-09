using MediatR;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Features.Requests.Queries
{
    public record GetUserOrdersRequest : IRequest<GetUserOrdersResponse>, IOrderFiltersParams
    {
        public OrderSortType SortType { get; init; }
    }
}