using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using MyVinted.Core.Application.Logic.Responses.Queries;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public record GetUsersRequest : UserFiltersParams, IRequest<GetUsersResponse>
    { }
}