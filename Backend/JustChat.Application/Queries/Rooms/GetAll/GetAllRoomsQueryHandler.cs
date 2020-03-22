using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Models.Rooms;
using MediatR;

namespace JustChat.Application.Queries.Rooms.GetAll
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IReadOnlyList<Room>>
    {
        private readonly IReadableRepository<Room> _roomRespository;

        public GetAllRoomsQueryHandler(IReadableRepository<Room> roomRespository)
        {
            _roomRespository = roomRespository;
        }

        public Task<IReadOnlyList<Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return _roomRespository.GetAllAsync();
        }
    }
}
