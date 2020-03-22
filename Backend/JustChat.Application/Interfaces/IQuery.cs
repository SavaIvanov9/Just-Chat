using MediatR;

namespace JustChat.Application.Interfaces
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
