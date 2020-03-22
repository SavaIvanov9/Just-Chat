using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class Message : Entity
    {
        private string _userId;
        private string _content;

        public Message(string userId, string content)
            : this()
        {
            UserId = userId;
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
