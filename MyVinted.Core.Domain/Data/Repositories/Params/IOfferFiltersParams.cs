using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Domain.Data.Repositories.Params
{
    public interface IOfferFiltersParams
    {
        string Title { get; init; }
        string CategoryId { get; init; }
        BoughtOfferStatus BoughtOfferStatus { get; init; }
        string UserId { get; init; }
        bool OnlyVerified { get; init; }

        OfferSortType SortType { get; init; }
    }
}