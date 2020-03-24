using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Features.Queries.GetRoom
{
    public class GetRoomQuery : IQuery<Room>
    {
        public long Id { get; set; }
    }
}
