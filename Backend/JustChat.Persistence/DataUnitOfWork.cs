using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Models.Messages;
using JustChat.Domain.Models.Rooms;
using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;
using JustChat.Persistence.Commands;

namespace JustChat.Persistence
{
    internal class DataUnitOfWork : IDataUnitOfWork
    {
        private readonly CommandDbContext _commandDbContext;

        public DataUnitOfWork(
            CommandDbContext commandDbContext,
            IMutatableRepository<User> users,
            IMutatableRepository<Token> tokens,
            IMutatableRepository<Room> rooms,
            IMutatableRepository<Message> messages)
        {
            _commandDbContext = commandDbContext;
            Users = users;
            Tokens = tokens;
            Rooms = rooms;
            Messages = messages;
        }

        public IMutatableRepository<User> Users { get; }
        
        public IMutatableRepository<Token> Tokens { get; }

        public IMutatableRepository<Room> Rooms { get; }

        public IMutatableRepository<Message> Messages { get; }

        public bool HasActiveTransaction => _commandDbContext.HasActiveTransaction;

        public async Task BeginTransactionAsync()
        {
            await _commandDbContext.BeginTransactionAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _commandDbContext.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _commandDbContext.RollbackTransactionAsync();
        }
    }
}