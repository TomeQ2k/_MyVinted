using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class UserAuthDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public double Rating { get; set; }
        public bool IsExternalUser { get; set; }
        public bool IsVerified { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; }
    }
}