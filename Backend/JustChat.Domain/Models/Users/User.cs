using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Users
{
    public class User : Entity, IAggregateRoot
    {
        private string _name;
        private string _password;

        public User(string name, string password)
            : this()
        {
            Name = name;
            Password = password;
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

        public string Password
        {
            get => _password;
            private set
            {
                _password = value;
                ValidateProperty(nameof(Password));
            }
        }
    }
}
