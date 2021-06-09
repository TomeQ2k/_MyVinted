using MediatR;
using MyVinted.Core.Application.Features.Responses.Queries;

namespace MyVinted.Core.Application.Features.Requests.Queries
{
    public record FetchCategoriesRequest : IRequest<FetchCategoriesResponse>
    { }
}