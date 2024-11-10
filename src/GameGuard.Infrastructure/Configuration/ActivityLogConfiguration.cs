using GameGuard.Domain.ActivityLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGuard.Infrastructure.Configuration
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlayerId).IsRequired();

            builder.Property(x => x.Action).IsRequired();

            builder.Property(x => x.Timestamp).IsRequired();

            builder.Property(x => x.IsSuspicious).IsRequired();

            builder.Property(x => x.IsReviewed).IsRequired();

            builder
                .HasOne(a => a.Player)
                .WithMany()
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
