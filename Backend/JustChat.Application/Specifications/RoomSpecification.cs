using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Specifications
{
    public class RoomSpecification : BaseSpecification<Room>
    {
        public static BaseSpecification<Room> FindById(string id) =>
            new RoomSpecification()
                .AddCriteria(n => n.Id == id);
    }
}
