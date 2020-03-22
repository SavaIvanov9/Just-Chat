using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Users
{
    public class User : Entity, IAggregateRoot
    {
        private string _name;

        public User(string name)
            : this()
        {
            Name = name;
        }

        protected User()
            : base(() => new EntityValidator())
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
    }
}
