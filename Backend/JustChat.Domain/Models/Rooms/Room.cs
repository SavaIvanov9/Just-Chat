using System.Collections.Generic;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class Room : Entity, IAggregateRoot
    {
        private string _name;
        private RoomType _type;

        public Room(string name, RoomType type)
            : this()
        {
            Name = name;
            Type = type;
        }

        protected Room()
            : base(() => new RoomValidator())
        {
        }

        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                ValidateProperty(nameof(Name));
            }
        }

        public RoomType Type
        {
            get => _type;
            private set
            {
                _type = value;
                ValidateProperty(nameof(_type));
            }
        }
    }
}
