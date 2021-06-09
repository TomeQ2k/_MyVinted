using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class OfferBookedSpecification : Specification<Offer>
    {
        public override Expression<Func<Offer, bool>> ToExpression()
            => offer => offer.BookingUserId != null;

        public static OfferBookedSpecification Create() => new OfferBookedSpecification();
    }
}