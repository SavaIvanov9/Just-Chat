using System;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Exceptions;
using JustChat.Application.Features.Commands.ValidateToken;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Token;
using Moq;
using Xunit;

namespace JustChat.Application.UnitTests.CommandHandlers
{
    public class ValidateTokenCommandHandlerTests : IClassFixture<ValidateTokenCommandHandlerFixture>
    {
        private readonly ValidateTokenCommandHandlerFixture _fixture;

        public ValidateTokenCommandHandlerTests(ValidateTokenCommandHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(1, "test123", "test456", 5)]
        [InlineData(2, "gdsdg", "xvdsfsdf", 10)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 50)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 100)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 1000)]
        public async Task ShoudReset_TokenExpiration_WhenToken_IsNotExpired(
            long id, string username, string password, int minutes)
        {
            var testToken = new Token(id, username, password, TimeSpan.FromMinutes(minutes));
            var initialValidTo = testToken.ValidTo;

            var spec = TokenSpecification.GetNotExpiredByValue(testToken.Value);

            _fixture.Data
                .Setup(x => x.Tokens.SingleOrDefaultAsync(It.IsAny<TokenSpecification>()))
                .Returns(Task.FromResult(testToken));

            _fixture.Configuration
                .Setup(x => x.ValidDurationInMinutes)
                .Returns(minutes);

            var command = new ValidateTokenCommand
            {
                Value = testToken.Value
            };

            var resultToken = await _fixture.Handler.Handle(command, CancellationToken.None);

            Assert.True(initialValidTo < resultToken.ValidTo);
        }

        [Theory]
        [InlineData(1, "test123", "test456", -5)]
        [InlineData(2, "www", "545354", -10)]
        [InlineData(3, "werfw", "f43t", -50)]
        [InlineData(3, "werfw", "f43t", -100)]
        [InlineData(3, "werfw", "f43t", -1000)]
        public async Task ShoudNotReset_TokenExpiration_WhenToken_IsExpired(
            long id, string username, string password, int minutes)
        {
            var testToken = new Token(id, username, password, TimeSpan.FromMinutes(minutes));
            var initialValidTo = testToken.ValidTo;

            var spec = TokenSpecification.GetNotExpiredByValue(testToken.Value);

            _fixture.Data
                .Setup(x => x.Tokens.SingleOrDefaultAsync(It.IsAny<TokenSpecification>()))
                .Returns(Task.FromResult(testToken));

            _fixture.Configuration
                .Setup(x => x.ValidDurationInMinutes)
                .Returns(minutes);

            var command = new ValidateTokenCommand
            {
                Value = testToken.Value
            };

            var resultToken = await _fixture.Handler.Handle(command, CancellationToken.None);

            Assert.True(initialValidTo == resultToken.ValidTo);
        }

        [Theory]
        [InlineData("teafssasfat123")]
        [InlineData("ADSADDAD")]
        [InlineData("123456")]
        [InlineData("tesasdsadt123")]
        [InlineData("^&$#^()(")]
        [InlineData("123123")]
        public async Task ShoudThrow_AuthenticationApplicationException_WhenInvalidToken(string value)
        {
            Token token = null;

            _fixture.Data
               .Setup(x => x.Tokens.SingleOrDefaultAsync(It.IsAny<TokenSpecification>()))
               .Returns(Task.FromResult(token));

            var command = new ValidateTokenCommand
            {
                Value = value
            };

            await Assert.ThrowsAsync<AuthenticationApplicationException>(
                () => _fixture.Handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData(1, "test123", "test456", 5)]
        [InlineData(2, "gdsdg", "xvdsfsdf", 10)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 50)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 100)]
        [InlineData(3, "sdfsd", "wedfsdfrwer", 1000)]
        public async Task ShoudNotThrow_Exception_WhenValidToken(
            long id, string username, string password, int minutes)
        {
            var testToken = new Token(id, username, password, TimeSpan.FromMinutes(minutes));

            var spec = TokenSpecification.GetNotExpiredByValue(testToken.Value);

            _fixture.Data
                .Setup(x => x.Tokens.SingleOrDefaultAsync(It.IsAny<TokenSpecification>()))
                .Returns(Task.FromResult(testToken));

            _fixture.Configuration
                .Setup(x => x.ValidDurationInMinutes)
                .Returns(minutes);

            var command = new ValidateTokenCommand
            {
                Value = testToken.Value
            };

            var exception = await Record.ExceptionAsync(
                () => _fixture.Handler.Handle(command, CancellationToken.None));

            Assert.True(exception == null);
        }
    }
}