using System.Collections.Generic;

namespace MyVinted.Core.Application.Dtos
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsVerified { get; set; }
        public bool IsAdmin { get; set; }
        public int FollowsCount { get; set; }
        public int OpinionsCount { get; set; }
        public double Rating { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<UserFollowDto> Followings { get; set; }
    }
}