using System;
using System.Linq.Expressions;
using MyVinted.Core.Application.Specifications;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : class, new()
    {
        public bool IsSatisfied(T model)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(model);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}