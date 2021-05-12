using System;
using System.Linq.Expressions;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class MinOfferPhotosCountSpecification : Specification<Offer>
    {
        public override Expression<Func<Offer, bool>> ToExpression()
           => offer => offer.OfferPhotos.Count - 1 > 0;

        public static MinOfferPhotosCountSpecification Create() => new MinOfferPhotosCountSpecification();
    }
}