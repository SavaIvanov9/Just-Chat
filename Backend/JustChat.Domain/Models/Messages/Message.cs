using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class Message : Entity, IAggregateRoot
    {
        private long _userId;
        private long _roomId;
        private string _content;

        public Message(long userId, long roomId, string content)
            : this()
        {
            UserId = userId;
            RoomId = roomId;
            Content = content;
        }

        protected Message()
            : base(() => new EntityValidator())
        {
        }

        public long UserId
        {
            get => _userId;
            private set
            {
                _userId = value;
                ValidateProperty(nameof(UserId));
            }
        }

        public long RoomId
        {
            get => _roomId;
            private set
            {
                _roomId = value;
                ValidateProperty(nameof(RoomId));
            }
        }

        public string Content
        {
            get => _content;
            private set
            {
                _content = value;
                ValidateProperty(nameof(Content));
            }
        }
    }
}
