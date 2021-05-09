using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Configs
{
    public class UserFollowConfig : IEntityTypeConfiguration<UserFollow>
    {
        public void Configure(EntityTypeBuilder<UserFollow> builder)
        {
            builder.HasKey(u => new { u.FollowerId, u.FollowingId });

            builder.HasOne(f => f.Follower)
                    .WithMany(u => u.Followers)
                    .HasForeignKey(f => f.FollowerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Following)
                    .WithMany(u => u.Followings)
                    .HasForeignKey(f => f.FollowingId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}