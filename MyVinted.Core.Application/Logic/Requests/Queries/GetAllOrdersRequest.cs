using MediatR;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public record GetAllOrdersRequest : PaginationRequest, IRequest<GetAllOrdersResponse>, IOrderFiltersParams
    {
        public OrderSortType SortType { get; init; }
        public OrderValidatedStatus ValidatedStatus { get; init; }
        public string Login { get; init; }
    }
}