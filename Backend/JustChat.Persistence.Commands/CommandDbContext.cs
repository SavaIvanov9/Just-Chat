using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Messages;
using JustChat.Domain.Models.Rooms;
using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JustChat.Persistence.Commands
{
    public class CommandDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        private bool _disposed;

        public CommandDbContext(DbContextOptions options, IMediator mediator)
             : base(options)
        {
            _mediator = mediator;
            _disposed = false;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CommandDbContext).Assembly);
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            UpdateCreatedEntities(now);
            UpdateEditedEntities(now);

            await PublishDomainEvents();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            UpdateCreatedEntities(now);
            UpdateEditedEntities(now);

            PublishDomainEvents().GetAwaiter().GetResult();

            var result = base.SaveChanges();
            return result;
        }

        public async Task BeginTransactionAsync()
        {
            if (HasActiveTransaction)
            {
                throw new Exception("There is active transaction that has not finished.");
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task RollbackTransactionAsync()
        {
            if (HasActiveTransaction)
            {
                try
                {
                    await _currentTransaction.RollbackAsync();
                }
                finally
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task CommitAsync()
        {
            if (HasActiveTransaction == false)
            {
                throw new InvalidOperationException("Not current transaction.");
            }

            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await _currentTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_disposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (HasActiveTransaction)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }

            _disposed = true;

            base.Dispose();
        }

        private void UpdateCreatedEntities(DateTime time)
        {
            var entries = ChangeTracker
                .Entries<IEntity>();

            var createdEntities = entries.Where(e => e.State == EntityState.Added);

            foreach (var entry in createdEntities)
            {
                entry.Property(nameof(IEntity.CreatedOn)).CurrentValue = time;
                entry.Property(nameof(IEntity.ModifiedOn)).CurrentValue = time;
            }
        }

        private void UpdateEditedEntities(DateTime time)
        {
            var entries = ChangeTracker
                .Entries<IEntity>();

            var editedEntities = entries.Where(e => e.State == EntityState.Modified);

            foreach (var entry in editedEntities)
            {
                entry.Property(nameof(IEntity.ModifiedOn)).CurrentValue = time;
            }
        }

        private async Task PublishDomainEvents()
        {
            var entities = ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.Entity?.DomainEvents?.Any() == true);

            if (entities?.Count() > 0)
            {
                var tasks = entities.Select(x => x.Entity.InvokeEvents(@event =>
                {
                    return _mediator.Publish(@event);
                }));

                await Task.WhenAll(tasks);

                await PublishDomainEvents();
            }
        }
    }
}
