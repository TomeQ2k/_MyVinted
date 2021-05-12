using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class OfferAuctionAcceptedSpecification : Specification<OfferAuction>
    {
        public override Expression<Func<OfferAuction, bool>> ToExpression()
            => offerAuction => offerAuction.IsAccepted;

        public static OfferAuctionAcceptedSpecification Create() => new OfferAuctionAcceptedSpecification();
    }
}