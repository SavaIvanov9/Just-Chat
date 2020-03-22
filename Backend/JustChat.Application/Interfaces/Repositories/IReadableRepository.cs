using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustChat.Application.Interfaces.Repositories
{
    public interface IReadableRepository<T>
        where T : class
    {
        Task<T> GetAsync(string id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);

        Task<bool> AnyAsync(ISpecification<T> spec);
    }
}
