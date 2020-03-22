using System.Threading;
using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;


namespace JustChat.Application.Validation
{
    public class AsyncValidator : PropertyValidator
    {
        public AsyncValidator(IStringSource stringSource)
            : base(stringSource)
        {
        }

        public AsyncValidator(string errorMessage)
            : base(errorMessage)
        {
        }

        public override bool ShouldValidateAsync(FluentValidation.ValidationContext context)
        {
            return context.IsAsync();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return IsValidAsync(context, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
