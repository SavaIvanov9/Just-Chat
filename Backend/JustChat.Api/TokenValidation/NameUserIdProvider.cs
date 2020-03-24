using System.Linq;
using JustChat.Domain.Models.Users;
using Microsoft.AspNetCore.SignalR;

namespace JustChat.Api
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims?
                .Where(x => x.Type == nameof(User.Id))
                .Select(x => x.Value).FirstOrDefault();
        }
    }
}