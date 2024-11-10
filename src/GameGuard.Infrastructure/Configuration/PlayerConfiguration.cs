using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGuard.Infrastructure.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Username).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Status).IsRequired().HasConversion<string>();

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Username).IsUnique();

            builder.HasMany<ActivityLog>().WithOne().HasForeignKey(a => a.PlayerId);

            builder.HasData(
                new Player(1, "Player1", PlayerStatusType.Active),
                new Player(2, "Player2", PlayerStatusType.Active),
                new Player(3, "Player3", PlayerStatusType.Suspicious),
                new Player(4, "Player4", PlayerStatusType.Banned)
            );
        }
    }
}
