using MediatR;

namespace JustChat.Application.Interfaces
{
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
