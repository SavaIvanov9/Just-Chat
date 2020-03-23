using System;
using System.Linq.Expressions;
using JustChat.Application.Enums;

namespace JustChat.Application.Models.Ordering
{
    public class OrderBy<T>
        where T : class
    {
        public OrderBy(OrderType type, Expression<Func<T, object>> expression)
        {
            Type = type;
            Expression = expression;
        }

        public OrderType Type { get; }

        public Expression<Func<T, object>> Expression { get; }
    }
}