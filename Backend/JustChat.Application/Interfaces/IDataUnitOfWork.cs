using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    }
}
