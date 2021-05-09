using System.Collections.Generic;
using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.ServiceUtils
{
    public static class FollowersUtils
    {
        public static IEnumerable<string> GetFollowersIds(User user)
            => user?.Followings.Select(f => f.FollowerId);
    }
}