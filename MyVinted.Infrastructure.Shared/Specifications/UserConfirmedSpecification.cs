using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class UserConfirmedSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
            => user => user.EmailConfirmed;

        public static UserConfirmedSpecification Create() => new UserConfirmedSpecification();
    }
}