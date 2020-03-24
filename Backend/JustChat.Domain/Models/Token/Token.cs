using System;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Token
{
    public class Token : Entity, IAggregateRoot
    {
        private long _userId;
        private string _value;
        private DateTime _validFrom;
        private DateTime _validTo;

        public Token(
            long userId, string username,
            string password, TimeSpan expiration)
            : this()
        {
            UserId = userId;
            Value = $"{username}-{password}-{Guid.NewGuid()}";
            ValidFrom = DateTime.UtcNow;
            ValidTo = ValidFrom.Add(expiration);
        }

        public Token()
            : base(() => new EntityValidator())
        {
        }

        public long UserId
        {
            get => _userId;
            private set
            {
                _userId = value;
                ValidateProperty(nameof(UserId));
            }
        }

        public string Value
        {
            get => _value;
            private set
            {
                _value = value;
                ValidateProperty(nameof(Value));
            }
        }

        public DateTime ValidFrom
        {
            get => _validFrom;
            private set
            {
                _validFrom = value;
                ValidateProperty(nameof(ValidFrom));
            }
        }

        public DateTime ValidTo
        {
            get => _validTo;
            private set
            {
                _validTo = value;
                ValidateProperty(nameof(ValidTo));
            }
        }

        public void Encrypt(IEncryptionService encryptionService, string encriptionKey)
        {
            Value = encryptionService.Encrypt(Value, encriptionKey);
        }

        public void Decrypt(IEncryptionService encryptionService, string encriptionKey)
        {
            Value = encryptionService.Decrypt(Value, encriptionKey);
        }
    }
}