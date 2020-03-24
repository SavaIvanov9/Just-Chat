using System;
using System.Security.Cryptography;
using System.Text;
using JustChat.Application.Interfaces.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JustChat.Application.Services
{
    internal class HashingService : IHashingService
    {
        public string GenerateHash(string text, string saltString)
        {
            var salt = Encoding.ASCII.GetBytes(saltString);

            var hashed = KeyDerivation.Pbkdf2(
                password: text,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256);

            var result = Convert.ToBase64String(hashed);

            return result;
        }

        public string GenerateHash(string text)
        {
            var salt = GetRandomSalt();

            var hashed = KeyDerivation.Pbkdf2(
                password: text,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256);

            var result = Convert.ToBase64String(hashed);

            return result;
        }

        private byte[] GetRandomSalt()
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}