using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Configs
{
    public class OfferConfig : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasOne(o => o.Category)
                    .WithMany(c => c.Offers)
                    .HasForeignKey(o => o.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Owner)
                    .WithMany(u => u.Offers)
                    .HasForeignKey(o => o.OwnerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OfferPhotos)
                    .WithOne(p => p.Offer)
                    .HasForeignKey(p => p.OfferId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.OfferAuction)
                    .WithOne(a => a.Offer)
                    .HasForeignKey<OfferAuction>(o => o.OfferId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(o => o.BookingUser)
                    .WithMany(u => u.BookedOffers)
                    .HasForeignKey(o => o.BookingUserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}