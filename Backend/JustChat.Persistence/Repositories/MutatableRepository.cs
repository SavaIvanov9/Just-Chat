using System.Threading.Tasks;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Base;
using JustChat.Persistence.Interfaces;
using JustChat.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JustChat.Persistence.Commands.Repositories
{
    public class MutatableRepository<TEntity> : ReadableRepository<TEntity>, IMutatableRepository<TEntity>
         where TEntity : Entity, IAggregateRoot
    {
        public MutatableRepository(
            CommandDbContext dbContext,
            ISpecificationEvaluationService<TEntity> specificationEvaluationService)
            : base(dbContext, specificationEvaluationService)
        {
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
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
