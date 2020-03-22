using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace JustChat.Persistence.Commands.Repositories
{
    public class MutatableRepository<TEntity> : IMutatableRepository<TEntity>
         where TEntity : Entity, IAggregateRoot
    {
        private readonly CommandDbContext _dbContext;

        public MutatableRepository(CommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _dbContext.Set<TEntity>().AddAsync(entity);
            return entry.Entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
