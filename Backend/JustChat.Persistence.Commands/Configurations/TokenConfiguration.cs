using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustChat.Persistence.Commands.Configurations
{
    internal class TokenConfiguration : BaseConfiguration<Token>
    {
        public override void Configure(EntityTypeBuilder<Token> builder)
        {
            base.Configure(builder);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
