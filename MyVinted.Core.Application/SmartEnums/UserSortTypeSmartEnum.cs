using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Linq;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class UserSortTypeSmartEnum : SmartEnum<UserSortTypeSmartEnum>
    {
        protected UserSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly UserSortTypeSmartEnum VerifiedDescending = new VerifiedDescendingType();
        public static readonly UserSortTypeSmartEnum OpinionsCountDescending = new OpinionsCountDescendingType();
        public static readonly UserSortTypeSmartEnum FollowsCountDescending = new FollowsCountDescendingType();
        public static readonly UserSortTypeSmartEnum RatingDescending = new RatingDescendingType();

        public abstract IQueryable<User> Sort(IQueryable<User> users);

        private sealed class VerifiedDescendingType : UserSortTypeSmartEnum
        {
            public VerifiedDescendingType() : base(nameof(VerifiedDescending), (int)UserSortType.VerifiedDescending) { }

            public override IQueryable<User> Sort(IQueryable<User> users)
                => users.OrderByDescending(u => u.UserRoles.Any(ur => ur.Role.Name == Constants.PremiumRole));
        }

        private sealed class OpinionsCountDescendingType : UserSortTypeSmartEnum
        {
            public OpinionsCountDescendingType() : base(nameof(OpinionsCountDescending), (int)UserSortType.OpinionsCountDescending) { }

            public override IQueryable<User> Sort(IQueryable<User> users)
                => users.OrderByDescending(u => u.Opinions.Count);
        }

        private sealed class FollowsCountDescendingType : UserSortTypeSmartEnum
        {
            public FollowsCountDescendingType() : base(nameof(FollowsCountDescending), (int)UserSortType.FollowsCountDescending) { }

            public override IQueryable<User> Sort(IQueryable<User> users)
                => users.OrderByDescending(u => u.Followings.Count);
        }

        private sealed class RatingDescendingType : UserSortTypeSmartEnum
        {
            public RatingDescendingType() : base(nameof(RatingDescending), (int)UserSortType.RatingDescending) { }

            public override IQueryable<User> Sort(IQueryable<User> users)
                => users.OrderByDescending(u => u.Opinions.Count != 0 ? u.Opinions.Select(o => o.Rating).Sum() / u.Opinions.Count : 0);
        }
    }
}