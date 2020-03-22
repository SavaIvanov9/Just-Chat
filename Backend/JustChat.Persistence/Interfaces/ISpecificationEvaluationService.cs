using System.Linq;
using JustChat.Application.Interfaces;

namespace JustChat.Persistence.Interfaces
{
    public interface ISpecificationEvaluationService<T>
        where T : class
    {
        IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification);
    }
}
