using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class Message : Entity, IAggregateRoot
    {
        private string _userId;
        private string _roomId;
        private string _content;

        public Message(string userId, string roomId, string content)
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

        public string UserId
        {
            get => _userId;
            private set
            {
                _userId = value;
                ValidateProperty(nameof(UserId));
            }
        }

        public string RoomId
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
