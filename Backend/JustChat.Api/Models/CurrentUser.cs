using JustChat.Api.Interfaces;

namespace JustChat.Api.Models
{
    internal class CurrentUser : ICurrentUser
    {
        public long Id { get; set; }
    }
}
