using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Role : IdentityRole<string>
    {
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();

        public static Role Create(RoleName roleName) => new Role
        {
            Id = Utils.Id(),
            Name = Utils.EnumToString<RoleName>(roleName)
        };
    }
}