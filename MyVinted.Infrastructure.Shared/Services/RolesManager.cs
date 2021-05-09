using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class RolesManager : IRolesManager
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;

        public RolesManager(RoleManager<Role> roleManager, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AdmitRole(RoleName roleName, User user)
            => user == null ? false : (await userManager.AddToRoleAsync(user, Utils.EnumToString(roleName))).Succeeded;

        public async Task<bool> RevokeRole(RoleName roleName, User user)
            => user == null ? false : (await userManager.RemoveFromRoleAsync(user, Utils.EnumToString(roleName))).Succeeded;

        public async Task<bool> CreateRole(RoleName roleName)
        {
            if (await roleManager.RoleExistsAsync(Utils.EnumToString(roleName)))
                return false;

            var role = Role.Create(roleName);

            return (await roleManager.CreateAsync(role)).Succeeded;
        }

        public async Task<bool> IsPermitted(RoleName roleName, int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            return user == null ? false : await userManager.IsInRoleAsync(user, Utils.EnumToString(roleName));
        }

        public bool IsPermitted(RoleName roleName, User user)
            => user.UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString(roleName));
    }
}