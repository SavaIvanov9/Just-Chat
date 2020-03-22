using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Api.Models.Rooms;
using JustChat.Application.Queries.Rooms.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAll()
        {
            var rooms = await Mediator.Send(new GetAllRoomsQuery());

            var result = rooms.Select(x =>
                new RoomResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type.Name
                });

            return this.Ok(result);
        }
    }
}
