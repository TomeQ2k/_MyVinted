using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class UserFollowStatusSmartEnum : SmartEnum<UserFollowStatusSmartEnum>
    {
        protected UserFollowStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly UserFollowStatusSmartEnum All = new AllType();
        public static readonly UserFollowStatusSmartEnum Followed = new FollowedType();
        public static readonly UserFollowStatusSmartEnum Following = new FollowingType();
        public static readonly UserFollowStatusSmartEnum NotFollowed = new NotFollowedType();
        public static readonly UserFollowStatusSmartEnum NotFollowing = new NotFollowingType();

        public abstract IQueryable<User> Filter(IQueryable<User> users, string currentUserId);

        private sealed class AllType : UserFollowStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)UserFollowStatus.All) { }

            public override IQueryable<User> Filter(IQueryable<User> users, string currentUserId) => users;
        }

        private sealed class FollowedType : UserFollowStatusSmartEnum
        {
            public FollowedType() : base(nameof(Followed), (int)UserFollowStatus.Followed) { }

            public override IQueryable<User> Filter(IQueryable<User> users, string currentUserId)
                => users.Where(u => u.Followings.Any(f => f.FollowerId == currentUserId));
        }

        private sealed class FollowingType : UserFollowStatusSmartEnum
        {
            public FollowingType() : base(nameof(Following), (int)UserFollowStatus.Following) { }

            public override IQueryable<User> Filter(IQueryable<User> users, string currentUserId)
                => users.Where(u => u.Followers.Any(f => f.FollowingId == currentUserId));
        }

        private sealed class NotFollowedType : UserFollowStatusSmartEnum
        {
            public NotFollowedType() : base(nameof(NotFollowed), (int)UserFollowStatus.NotFollowed) { }

            public override IQueryable<User> Filter(IQueryable<User> users, string currentUserId)
                => users.Where(u => !u.Followings.Any(f => f.FollowerId == currentUserId));
        }

        private sealed class NotFollowingType : UserFollowStatusSmartEnum
        {
            public NotFollowingType() : base(nameof(NotFollowing), (int)UserFollowStatus.NotFollowing) { }

            public override IQueryable<User> Filter(IQueryable<User> users, string currentUserId)
                => users.Where(u => !u.Followers.Any(f => f.FollowingId == currentUserId));
        }
    }
}