using MyVinted.Core.Common.Enums;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyRolesManager
    {
        Task<bool> IsPermitted(RoleName roleName, int userId);
        bool IsPermitted(RoleName roleName, User user);
    }
}