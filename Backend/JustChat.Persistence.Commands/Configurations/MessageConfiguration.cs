using JustChat.Domain.Models.Messages;
using JustChat.Domain.Models.Rooms;
using JustChat.Domain.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustChat.Persistence.Commands.Configurations
{
    internal class MessageConfiguration : BaseConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.RoomId);
        }
    }
}
