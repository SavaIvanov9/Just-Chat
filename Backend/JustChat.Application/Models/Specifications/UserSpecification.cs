using JustChat.Application.Models.Specifications.Base;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Models.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public static BaseSpecification<User> GetById(long id) =>
            new UserSpecification()
                .AddCriteria(n => n.Id == id);

        public static BaseSpecification<User> GetByName(string name) =>
            new UserSpecification()
                .AddCriteria(n => n.Name == name);

        public static BaseSpecification<User> GetByCredentials(string name, string password) =>
            new UserSpecification()
                .AddCriteria(n => n.Name == name)
                .AddCriteria(x => x.Password == password);
    }
}
