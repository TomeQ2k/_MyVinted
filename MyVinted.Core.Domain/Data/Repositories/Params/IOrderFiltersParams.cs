using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Domain.Data.Repositories.Params
{
    public interface IOrderFiltersParams
    {
        OrderSortType SortType { get; init; }
    }
}