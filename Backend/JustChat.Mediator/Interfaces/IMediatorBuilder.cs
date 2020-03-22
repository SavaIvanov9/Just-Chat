using System;
using System.Collections.Generic;
using System.Text;

namespace JustChat.Mediator.Interfaces
{
    public interface IMediatorBuilder
    {
        IMediatorBuilder WithValidationBehavior();

        IMediatorBuilder WithLoggingBehavior();

        IMediatorBuilder WithPersistableBehavior();
    }
}
