using JustChat.Domain.Models.Rooms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustChat.Persistence.Commands.Configurations
{
    internal class RoomConfiguration : BaseConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(x => x.Type);
        }
    }
}
