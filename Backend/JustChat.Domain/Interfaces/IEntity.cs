using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustChat.Domain.Interfaces
{
    public interface IEntity
    {
        string Id { get; }

        DateTime CreatedOn { get; }

        DateTime? ModifiedOn { get; }

        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        Task InvokeEvents(Func<IDomainEvent, Task> invoke);
    }
}