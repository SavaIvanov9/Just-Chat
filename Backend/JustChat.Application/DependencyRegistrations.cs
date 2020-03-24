using JustChat.Application.Extensions;
using JustChat.Application.Interfaces.Services;
using JustChat.Application.Models.Configurations;
using JustChat.Application.Services;
using JustChat.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Persistence
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterApplicationDepenencies(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = configuration.Bind<TokenConfiguration>(nameof(TokenConfiguration));
            services.AddSingleton(tokenConfig);

            var passConfig = configuration.Bind<PasswordConfiguration>(nameof(PasswordConfiguration));
            services.AddSingleton(passConfig);

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IHashingService, HashingService>();

            return services;
        }
    }
}
