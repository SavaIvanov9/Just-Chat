using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using MediatR;

namespace JustChat.Mediator.Behaviors
{
    public class PersistableBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IDataUnitOfWork _data;

        public PersistableBehavior(IDataUnitOfWork data)
        {
            _data = data;
        }

        public async Task<TResponse> Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.GetType().IsSubclassOf(typeof(ICommand<TResponse>)) == false
                || _data.HasActiveTransaction)
            {
                return await next();
            }

            try
            {
                await _data.BeginTransactionAsync();
                var result = await next();
                await _data.CommitAsync();

                return result;
            }
            catch
            {
                await _data.RollbackTransactionAsync();
                throw;
            }
        }
    }
}