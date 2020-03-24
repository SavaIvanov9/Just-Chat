using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Models.Users;
using MediatR;

namespace JustChat.Application.Features.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IReadableRepository<User> _respository;

        public GetUserQueryHandler(IReadableRepository<User> repository)
        {
            _respository = repository;
        }

        public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return _respository.GetAsync(request.Id);
        }
    }
}
