using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Features.Requests.Queries.Params
{
    public abstract record MessengerFiltersParams : PaginationRequest, IMessengerFiltersParams
    {
        public string Username { get; init; }
    }
}