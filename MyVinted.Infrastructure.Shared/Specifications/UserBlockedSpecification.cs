using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class UserBlockedSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
            => user => user.IsBlocked;

        public static UserBlockedSpecification Create() => new UserBlockedSpecification();
    }
}