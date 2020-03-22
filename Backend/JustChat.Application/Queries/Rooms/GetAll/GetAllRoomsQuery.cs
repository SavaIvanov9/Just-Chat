using System.Collections.Generic;
using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Queries.Rooms.GetAll
{
    public class GetAllRoomsQuery : IQuery<IReadOnlyList<Room>>
    {
    }
}
