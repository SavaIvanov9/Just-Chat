using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Users;
using MediatR;

namespace JustChat.Application.Features.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IDataUnitOfWork _data;

        public CreateUserCommandHandler(IDataUnitOfWork data)
        {
            _data = data;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name);
            return await _data.Users.AddAsync(user);
        }
    }
}
