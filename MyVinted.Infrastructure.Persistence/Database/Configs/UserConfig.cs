using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();

            builder.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Opinions)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.OpinionsCreated)
                    .WithOne(o => o.Creator)
                    .HasForeignKey(o => o.CreatorId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.BalanceAccount)
                    .WithOne(a => a.Account)
                    .HasForeignKey<BalanceAccount>(a => a.AccountId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}