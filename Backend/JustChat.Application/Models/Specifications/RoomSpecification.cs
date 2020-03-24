using JustChat.Application.Models.Specifications.Base;
using JustChat.Domain.Models.Rooms;

namespace JustChat.Application.Models.Specifications
{
    public class RoomSpecification : BaseSpecification<Room>
    {
        public static BaseSpecification<Room> GetById(long id) =>
            new RoomSpecification()
                .AddCriteria(x => x.Id == id);

        public static BaseSpecification<Room> GetAllSorted() =>
           new RoomSpecification()
                .ApplyOrderBy(x => x.Type.Value)
                .ApplyThenOrderBy(x => x.Name);
    }
}
