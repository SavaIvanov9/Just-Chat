using System;

namespace JustChat.Application.Models.Configurations
{
    public class PasswordConfiguration
    {
        private string _saltString;

        public string SaltString
        {
            get => _saltString;
            private set => _saltString = value ?? throw new ArgumentNullException(nameof(SaltString));
        }
    }
}
