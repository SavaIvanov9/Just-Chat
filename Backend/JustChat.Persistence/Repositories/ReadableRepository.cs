using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Persistence.Commands;
using JustChat.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustChat.Persistence.Repositories
{
    public class ReadableRepository<TEntity> : IReadableRepository<TEntity>
         where TEntity : class
    {
        protected readonly CommandDbContext _dbContext;
        private readonly ISpecificationEvaluationService<TEntity> _specificationEvaluationService;

        public ReadableRepository(
            CommandDbContext dbContext,
            ISpecificationEvaluationService<TEntity> specificationEvaluationService)
        {
            _dbContext = dbContext;
            _specificationEvaluationService = specificationEvaluationService;
        }

        public async Task<bool> AnyAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).AnyAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> GetAsync(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return _specificationEvaluationService.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
