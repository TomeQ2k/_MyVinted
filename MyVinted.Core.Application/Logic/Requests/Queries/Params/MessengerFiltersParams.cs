using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Logic.Requests.Queries.Params
{
    public abstract record MessengerFiltersParams : PaginationRequest, IMessengerFiltersParams
    {
        public string Username { get; init; }
    }
}