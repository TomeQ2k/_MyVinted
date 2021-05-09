using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Domain.Data.Repositories.Params
{
    public interface IUserFiltersParams
    {
        string Name { get; init; }
        UserFollowStatus FollowStatus { get; init; }
        bool OnlyVerified { get; init; }
        string CurrentUserId { get; init; }

        UserSortType SortType { get; init; }
    }
}