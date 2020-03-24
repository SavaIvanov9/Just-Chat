using FluentValidation;
using JustChat.Domain.Interfaces;

namespace JustChat.Domain.Models.Base
{
    public class EntityValidator<T> : AbstractValidator<T>
        where T : IEntity
    {
        public EntityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
              .NotEmpty();
        }
    }
}
