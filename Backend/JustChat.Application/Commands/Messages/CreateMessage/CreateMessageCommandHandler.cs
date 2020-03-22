using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JustChat.Application.Commands.Messages.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Unit>
    {
        public Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
