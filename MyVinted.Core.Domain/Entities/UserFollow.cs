namespace MyVinted.Core.Domain.Entities
{
    public class UserFollow
    {
        public string FollowerId { get; protected set; }
        public string FollowingId { get; protected set; }

        public virtual User Follower { get; protected set; }
        public virtual User Following { get; protected set; }

        public static UserFollow Create(string followerId, string followingId) => new UserFollow
        {
            FollowerId = followerId,
            FollowingId = followingId
        };
    }
}