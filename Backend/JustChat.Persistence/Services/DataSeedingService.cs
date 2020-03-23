using System.Linq;
using JustChat.Domain.Models.Rooms;
using JustChat.Persistence.Commands;
using JustChat.Persistence.Interfaces;

namespace JustChat.Persistence.Services
{
    internal class DataSeedingService : IDataSeedingService
    {
        private readonly CommandDbContext _context;

        public DataSeedingService(CommandDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            SeedRooms();
        }

        private void SeedRooms()
        {
            if (_context.Rooms.Any())
            {
                return;
            }

            var rooms = new Room[]
            {
                new Room("Family", RoomType.Default()),
                new Room("Friends", RoomType.Other()),
                new Room("Work", RoomType.Other()),
            };

            _context.Rooms.AddRange(rooms);
            _context.SaveChanges();
        }
    }
}
