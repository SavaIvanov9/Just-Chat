using JustChat.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace JustChat.Application.Services
{
    internal class EncryptionService : IEncryptionService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            this._dataProtectionProvider = dataProtectionProvider;
        }

        public string Decrypt(string text, string key)
        {
            var protector = _dataProtectionProvider.CreateProtector(key);
            return protector.Unprotect(text);
        }

        public string Encrypt(string text, string key)
        {
            var protector = _dataProtectionProvider.CreateProtector(key);
            return protector.Protect(text);
        }
    }
}
