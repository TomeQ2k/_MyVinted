using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IUserService : IReadOnlyUserService
    {
        Task<(bool, UserFollow)> FollowUser(string userId);
    }
}