using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using JustChat.Domain.Exceptions;

namespace JustChat.Domain.Models.Base
{
    public abstract class Validatable
    {
        private readonly Func<IValidator> _validatorFactory;

        protected Validatable(Func<IValidator> validatorFactory)
        {
            _validatorFactory = validatorFactory ?? throw new ArgumentException(nameof(validatorFactory));
        }

        protected void Validate()
        {
            var validator = _validatorFactory.Invoke();
            var result = validator.Validate(this);

            ProcessValidationResult(result);
        }

        protected void ValidateProperty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            var context = new ValidationContext<Validatable>(
                this, new PropertyChain(), new MemberNameValidatorSelector(new[] { name }));
            var validator = _validatorFactory.Invoke();
            var result = validator.Validate(context);

            ProcessValidationResult(result);
        }

        private void ProcessValidationResult(ValidationResult result)
        {
            if (result.IsValid == false)
            {
                var error = result.Errors.First();
                throw new ValidationDomainException(error.ErrorMessage, error.ErrorMessage);
            }
        }
    }
}
