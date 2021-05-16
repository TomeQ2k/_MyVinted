using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class OfferAllowBiddingSpecification : Specification<Offer>
    {
        public override Expression<Func<Offer, bool>> ToExpression()
            => offer => offer.AllowBidding;

        public static OfferAllowBiddingSpecification Create() => new OfferAllowBiddingSpecification();
    }
}
