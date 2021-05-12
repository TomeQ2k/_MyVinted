using System;
using System.Linq;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class AddedOneOpinionPerUserSpecification : Specification<User>
    {
        private readonly string currentUserId;

        private AddedOneOpinionPerUserSpecification(string currentUserId)
        {
            this.currentUserId = currentUserId;
        }

        public override Expression<Func<User, bool>> ToExpression()
            => user => user.Opinions.Any(o => o.CreatorId == currentUserId);

        public static AddedOneOpinionPerUserSpecification Create(string currentUserId) => new AddedOneOpinionPerUserSpecification(currentUserId);
    }
}