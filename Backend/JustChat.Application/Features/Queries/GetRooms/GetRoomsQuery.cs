using System.Collections.Generic;
using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Features.Queries.Rooms.GetRooms
{
    public class GetRoomsQuery : IQuery<IReadOnlyList<Room>>
    {
    }
}
