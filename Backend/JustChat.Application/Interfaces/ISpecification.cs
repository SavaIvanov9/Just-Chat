using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JustChat.Application.Interfaces
{
    public interface ISpecification<T>
        where T : class
    {
        IReadOnlyCollection<Expression<Func<T, bool>>> Criterias { get; }
    }
}
