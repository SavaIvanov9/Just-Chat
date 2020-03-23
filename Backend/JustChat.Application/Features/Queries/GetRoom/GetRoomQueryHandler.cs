using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Models.Rooms;
using MediatR;

namespace JustChat.Application.Features.Queries.GetRoom
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Room>
    {
        private readonly IReadableRepository<Room> _roomRespository;

        public GetRoomQueryHandler(IReadableRepository<Room> roomRespository)
        {
            _roomRespository = roomRespository;
        }

        public Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            return _roomRespository.GetAsync(request.Id);
        }
    }
}
