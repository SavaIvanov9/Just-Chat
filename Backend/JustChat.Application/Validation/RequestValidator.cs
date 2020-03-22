using FluentValidation;

namespace JustChat.Application.Validation
{
    public abstract class RequestValidator<TRequest> : AbstractValidator<TRequest>
    {
        public RequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}