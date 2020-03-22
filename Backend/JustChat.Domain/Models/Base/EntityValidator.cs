using FluentValidation;
using JustChat.Domain.Interfaces;

namespace JustChat.Domain.Models.Base
{
    public class EntityValidator : AbstractValidator<IEntity>
    {
        public EntityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
              .NotEmpty();
        }
    }
}
