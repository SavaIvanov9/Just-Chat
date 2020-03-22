using System;

namespace JustChat.Persistence.Commands.Interfaces
{
    interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
