using Microsoft.Extensions.Configuration;

namespace JustChat.Application.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T Bind<T>(this IConfiguration configuration, string key)
            where T : class
        {
            return configuration.GetSection(key).Get<T>(options => options.BindNonPublicProperties = true);
        }
    }
}