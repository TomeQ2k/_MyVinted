using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Configs
{
    public class OfferLikeConfig : IEntityTypeConfiguration<OfferLike>
    {
        public void Configure(EntityTypeBuilder<OfferLike> builder)
        {
            builder.HasKey(l => new { l.OfferId, l.UserId });

            builder.HasOne(l => l.Offer)
                    .WithMany(o => o.OfferLikes)
                    .HasForeignKey(l => l.OfferId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.User)
                    .WithMany(u => u.OfferLikes)
                    .HasForeignKey(l => l.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}