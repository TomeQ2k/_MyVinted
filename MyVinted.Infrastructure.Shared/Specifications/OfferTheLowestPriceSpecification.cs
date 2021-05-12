using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class OfferTheLowestPriceSpecification : Specification<Offer>
    {
        public override Expression<Func<Offer, bool>> ToExpression()
            => offer => offer.Price == 1;

        public static OfferTheLowestPriceSpecification Create() => new OfferTheLowestPriceSpecification();
    }
}