﻿using System.Linq;
using JustChat.Application.Interfaces;
using JustChat.Persistence.Commands.Interfaces;

namespace JustChat.Persistence.Commands.Services
{
    internal class SpecificationEvaluationService<T> : ISpecificationEvaluationService<T>
        where T : class
    {
        public IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            query = specification
                .Criterias?
                .Aggregate(query, (current, criteria) => current.Where(criteria));

            return query;
        }
    }
}