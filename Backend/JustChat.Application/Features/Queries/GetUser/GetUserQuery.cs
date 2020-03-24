using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Features.Queries.GetUser
{
    public class GetUserQuery : IQuery<User>
    {
        public long Id { get; set; }
    }
}
