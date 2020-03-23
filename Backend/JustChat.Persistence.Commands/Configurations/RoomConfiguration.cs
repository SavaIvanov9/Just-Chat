using JustChat.Domain.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustChat.Persistence.Commands.Configurations
{
    internal class RoomConfiguration : BaseConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(
                entity => entity.Type,
                b =>
                {
                    b.Property(x => x.Name).HasColumnName($"{nameof(RoomType)}{nameof(RoomType.Name)}");
                    b.Property(x => x.Value).HasColumnName($"{nameof(RoomType)}{nameof(RoomType.Value)}");
                });
        }
    }
}
