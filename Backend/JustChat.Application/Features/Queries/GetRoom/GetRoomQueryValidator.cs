using FluentValidation;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Queries.GetRoom
{
    public class GetRoomQueryValidator : RequestValidator<GetRoomQuery>
    {
        public GetRoomQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
