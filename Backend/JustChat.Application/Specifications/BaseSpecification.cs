using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JustChat.Application.Interfaces;

namespace JustChat.Application.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
        where T : class
    {
        private readonly List<Expression<Func<T, bool>>> _criterias;

        public BaseSpecification()
        {
            _criterias = new List<Expression<Func<T, bool>>>();
        }

        public IReadOnlyCollection<Expression<Func<T, bool>>> Criterias => _criterias;

        public virtual BaseSpecification<T> AddCriteria(Expression<Func<T, bool>> criteria)
        {
            _criterias.Add(criteria);
            return this;
        }
    }
}
