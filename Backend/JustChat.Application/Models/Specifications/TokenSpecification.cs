using System;
using JustChat.Application.Models.Specifications.Base;
using JustChat.Domain.Models.Token;

namespace JustChat.Application.Models.Specifications
{
    public class TokenSpecification : BaseSpecification<Token>
    {
        public static BaseSpecification<Token> GetNotExpiredByValue(string value)
        {
            var now = DateTime.UtcNow;

            return new TokenSpecification()
                .AddCriteria(x => x.Value == value)
                .AddCriteria(x => x.ValidTo > now);
        }
    }
}
