using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Rooms;
using MediatR;

namespace JustChat.Application.Features.Queries.Rooms.GetRooms
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IReadOnlyList<Room>>
    {
        private readonly IReadableRepository<Room> _roomRespository;

        public GetRoomsQueryHandler(IReadableRepository<Room> roomRespository)
        {
            _roomRespository = roomRespository;
        }

        public Task<IReadOnlyList<Room>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            var spec = RoomSpecification.GetAllSorted();
            return _roomRespository.GetAllAsync(spec);
        }
    }
}
