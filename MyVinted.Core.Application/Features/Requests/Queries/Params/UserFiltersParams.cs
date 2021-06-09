using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Repositories.Params;

namespace MyVinted.Core.Application.Features.Requests.Queries.Params
{
    public abstract record UserFiltersParams : PaginationRequest, IUserFiltersParams
    {
        public string Name { get; init; }
        public UserFollowStatus FollowStatus { get; init; }
        public bool OnlyVerified { get; init; }
        public string CurrentUserId { get; init; }

        public UserSortType SortType { get; init; }
    }
}