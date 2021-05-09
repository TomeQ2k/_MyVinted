using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyVinted.Core.Domain.Entities;
using MyVinted.Infrastructure.Persistence.Database.Configs;

namespace MyVinted.Infrastructure.Persistence.Database
{
    public class DataContext : IdentityDbContext<User, Role, string,
       IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
       IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        #region tables

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Opinion> Opinions { get; set; }
        public virtual DbSet<OfferPhoto> OfferPhotos { get; set; }
        public virtual DbSet<OfferLike> OfferLikes { get; set; }
        public virtual DbSet<UserFollow> UserFollows { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<OfferAuction> OfferAuctions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<StripeToken> StripeTokens { get; set; }
        public virtual DbSet<BalanceAccount> BalanceAccounts { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<File> Files { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfig().Configure(modelBuilder.Entity<User>());
            new RoleConfig().Configure(modelBuilder.Entity<Role>());
            new UserRoleConfig().Configure(modelBuilder.Entity<UserRole>());

            new OfferConfig().Configure(modelBuilder.Entity<Offer>());
            new OfferLikeConfig().Configure(modelBuilder.Entity<OfferLike>());

            new UserFollowConfig().Configure(modelBuilder.Entity<UserFollow>());

            new MessageConfig().Configure(modelBuilder.Entity<Message>());

            new OrderConfig().Configure(modelBuilder.Entity<Order>());
            new CartConfig().Configure(modelBuilder.Entity<Cart>());

            new ConnectionConfig().Configure(modelBuilder.Entity<Connection>());
        }
    }
}