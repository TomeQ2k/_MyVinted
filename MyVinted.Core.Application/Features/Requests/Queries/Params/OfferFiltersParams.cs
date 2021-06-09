using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Features.Requests.Queries.Params
{
    public abstract record OfferFiltersParams : PaginationRequest, IOfferFiltersParams
    {
        public string Title { get; init; }
        public string CategoryId { get; init; }
        public BoughtOfferStatus BoughtOfferStatus { get; init; } = BoughtOfferStatus.NotBought;
        public string UserId { get; init; }
        public bool OnlyVerified { get; init; }

        public OfferSortType SortType { get; init; }
    }
}