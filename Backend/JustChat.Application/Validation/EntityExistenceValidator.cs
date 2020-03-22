using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;

namespace JustChat.Application.Validation
{
    public class EntityExistenceValidator<TEntity, TProperty> : AsyncValidator
      where TEntity : class
    {
        private readonly IReadableRepository<TEntity> _repository;
        private readonly Func<TProperty, ISpecification<TEntity>> _specification;
        private readonly bool _shouldExist;

        public EntityExistenceValidator(
            IReadableRepository<TEntity> repository,
            Func<TProperty, ISpecification<TEntity>> specification,
            bool shouldExist,
            string errorMessage)
            : base(errorMessage)
        {
            _repository = repository;
            _specification = specification;
            _shouldExist = shouldExist;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var property = (TProperty)context.PropertyValue;

            var exists = await _repository.AnyAsync(_specification(property));

            return _shouldExist == exists;
        }
    }
}