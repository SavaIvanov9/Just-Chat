using System;

namespace JustChat.Application.Models.Configurations
{
    public class TokenConfiguration
    {
        private string _encryptionKey;
        private int? _validDurationInMinutes;

        public virtual string EncryptionKey
        {
            get => _encryptionKey;
            private set => _encryptionKey = value ?? throw new ArgumentNullException(nameof(EncryptionKey));
        }

        public virtual int? ValidDurationInMinutes
        {
            get => _validDurationInMinutes;
            private set => _validDurationInMinutes = value ?? throw new ArgumentNullException(nameof(ValidDurationInMinutes));
        }

        public TimeSpan ValidDuration => TimeSpan.FromMinutes(ValidDurationInMinutes.Value);
    }
}
