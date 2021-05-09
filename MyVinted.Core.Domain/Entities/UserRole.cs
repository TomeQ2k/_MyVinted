using Microsoft.AspNetCore.Identity;

namespace MyVinted.Core.Domain.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; protected set; }
        public virtual Role Role { get; protected set; }
    }
}