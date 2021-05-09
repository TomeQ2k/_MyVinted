using MyVinted.Core.Application.SmartEnums;
using MyVinted.Core.Domain.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<User>> GetFilteredUsers(IUserFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var users = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
                users = users.Where(u =>
                    u.Email.ToLower().Contains(filters.Name.ToLower()) ||
                    u.UserName.ToLower().Contains(filters.Name.ToLower()));

            users = UserFollowStatusSmartEnum.FromValue((int)filters.FollowStatus)
                .Filter(users, filters.CurrentUserId);

            if (filters.OnlyVerified)
                users = users.Where(u => u.UserRoles.Any(ur => ur.Role.Name == Constants.PremiumRole));

            users = UserSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(users);

            return await users.ToPagedList<User>(pagination.PageNumber, pagination.PageSize);
        }
    }
}