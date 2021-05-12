using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class UserIsExternalSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
            => user => user.IsExternal() && !user.IsRegistered();

        public static UserIsExternalSpecification Create() => new UserIsExternalSpecification();
    }
}