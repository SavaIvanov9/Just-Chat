using System;
using Microsoft.IdentityModel.Tokens;

namespace JustChat.Api
{
    public class AuthenticationToken : SecurityToken
    {
        public AuthenticationToken(string id, DateTime validFrom, DateTime validTo)
        {
            Id = id;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public override string Id { get; }

        public override string Issuer { get; }

        public override SecurityKey SecurityKey { get; }

        public override SecurityKey SigningKey { get; set; }

        public override DateTime ValidFrom { get; }

        public override DateTime ValidTo { get; }
    }
}
