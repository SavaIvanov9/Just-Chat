using System.Threading.Tasks;
using JustChat.Domain.Interfaces;

namespace JustChat.Application.Interfaces.Repositories
{
    public interface IMutatableRepository<T> : IReadableRepository<T>
        where T : class, IAggregateRoot
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
