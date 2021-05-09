using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public string AvatarUrl { get; protected set; }
        public bool IsBlocked { get; protected set; }

        public virtual Cart Cart { get; protected set; }
        public virtual BalanceAccount BalanceAccount { get; protected set; }

        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();
        public virtual ICollection<Offer> Offers { get; protected set; } = new HashSet<Offer>();
        public virtual ICollection<OfferLike> OfferLikes { get; protected set; } = new HashSet<OfferLike>();
        public virtual ICollection<UserFollow> Followers { get; protected set; } = new HashSet<UserFollow>();
        public virtual ICollection<UserFollow> Followings { get; protected set; } = new HashSet<UserFollow>();
        public virtual ICollection<Opinion> Opinions { get; protected set; } = new HashSet<Opinion>();
        public virtual ICollection<Opinion> OpinionsCreated { get; protected set; } = new HashSet<Opinion>();
        public virtual ICollection<Notification> Notifications { get; protected set; } = new HashSet<Notification>();
        public virtual ICollection<Message> MessagesSent { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Message> MessagesReceived { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Connection> Connections { get; protected set; } = new HashSet<Connection>();
        public virtual ICollection<Order> Orders { get; protected set; } = new HashSet<Order>();
        public virtual ICollection<Offer> BookedOffers { get; protected set; } = new HashSet<Offer>();

        public static User Create(string email, string username) => new User
        {
            Id = Utils.Id(),
            Email = email,
            UserName = username
        };

        public void SetAvatarUrl(string avatarUrl) => AvatarUrl = avatarUrl;

        public bool IsRegistered() => UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString<RoleName>(RoleName.User));

        public bool IsExternal() => UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString<RoleName>(RoleName.ExternalUser));

        public bool IsVerified() => UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString<RoleName>(RoleName.Premium));

        public bool IsAdmin() => UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString<RoleName>(RoleName.Admin));

        public void SetCart(Cart cart) => Cart = cart;

        public void ToggleIsBlocked() => IsBlocked = !IsBlocked;
    }
}