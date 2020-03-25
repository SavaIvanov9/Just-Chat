using JustChat.Application.Features.Commands.ValidateToken;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Configurations;
using Moq;

namespace JustChat.Application.UnitTests.CommandHandlers
{
    public class ValidateTokenCommandHandlerFixture
    {
        public ValidateTokenCommandHandlerFixture()
        {
            Data = new Mock<IDataUnitOfWork>();
            Configuration = new Mock<TokenConfiguration>();
            Handler = new ValidateTokenCommandHandler(Data.Object, Configuration.Object);
        }

        public ValidateTokenCommandHandler Handler { get; private set; }

        public Mock<IDataUnitOfWork> Data { get; private set; }

        public Mock<TokenConfiguration> Configuration { get; private set; }
    }
}
