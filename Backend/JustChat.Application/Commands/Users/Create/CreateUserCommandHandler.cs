using System;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Domain.Models.Users;
using MediatR;

namespace JustChat.Application.Commands.Users.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
