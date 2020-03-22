using System;
using FluentValidation;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Application.Validation;

namespace JustChat.Application.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<TRequest, TProperty> Exists<TRequest, TProperty, TEntity>(
            this IRuleBuilder<TRequest, TProperty> rule,
            Func<TProperty, ISpecification<TEntity>> spec,
            IReadableRepository<TEntity> repo)
            where TEntity : class
        {
            return rule.SetValidator(new EntityExistenceValidator<TEntity, TProperty>(repo, spec, true, "{PropertyName} does not exist."));
        }

        public static IRuleBuilderOptions<TRequest, TProperty> DoesNotExist<TRequest, TProperty, TEntity>(
            this IRuleBuilder<TRequest, TProperty> rule,
            Func<TProperty, ISpecification<TEntity>> spec,
            IReadableRepository<TEntity> repo)
            where TEntity : class
        {
            return rule.SetValidator(new EntityExistenceValidator<TEntity, TProperty>(repo, spec, false, "{PropertyName} already exists."));
        }
    }
}
