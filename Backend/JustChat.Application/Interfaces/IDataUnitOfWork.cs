using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Models.Rooms;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Interfaces
{
    public interface IDataUnitOfWork
    {
        bool HasActiveTransaction { get; }

        Task BeginTransactionAsync();

        Task RollbackTransactionAsync();

        Task CommitAsync(CancellationToken cancellationToken = default);

        IMutatableRepository<User> Users { get; }

        IMutatableRepository<Room> Rooms { get; }

        IMutatableRepository<Message> Messages { get; }
    }
}
