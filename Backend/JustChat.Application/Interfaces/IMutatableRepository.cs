using System.Threading.Tasks;
using JustChat.Domain.Interfaces;

namespace JustChat.Application.Interfaces
{
    public interface IMutatableRepository<T>
        where T : class, IAggregateRoot
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
