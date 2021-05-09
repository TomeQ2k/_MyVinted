using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;

        public UserService(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
        }

        public async Task<User> GetUser(string userId)
            => await unitOfWork.UserRepository.Get(userId) ?? throw new EntityNotFoundException("User not found");

        public async Task<IPagedList<User>> GetUsers(UserFiltersParams filters)
            => await unitOfWork.UserRepository.GetFilteredUsers(filters, (filters.PageNumber, filters.PageSize));

        public async Task<(bool, UserFollow)> FollowUser(string userId)
        {
            var currentUser = await accountManager.GetCurrentUser();

            if (currentUser.Id == userId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            var user = await unitOfWork.UserRepository.Get(userId) ??
                       throw new EntityNotFoundException("User not found");
            var userFollow = user.Followings.FirstOrDefault(f => f.FollowerId == currentUser.Id);

            if (userFollow != null)
            {
                currentUser.Followers.Remove(userFollow);
                user.Followings.Remove(userFollow);

                return await unitOfWork.Complete()
                    ? (false, null)
                    : throw new ServerException("Unfollowing user failed");
            }

            userFollow = UserFollow.Create(currentUser.Id, user.Id);

            currentUser.Followers.Add(userFollow);
            user.Followings.Add(userFollow);

            return await unitOfWork.Complete()
                ? (true, userFollow)
                : throw new ServerException("Following user failed");
        }
    }
}