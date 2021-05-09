using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Common.Enums;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IRolesManager : IReadOnlyRolesManager
    {
        Task<bool> AdmitRole(RoleName roleName, User user);
        Task<bool> RevokeRole(RoleName roleName, User user);

        Task<bool> CreateRole(RoleName roleName);
    }
}