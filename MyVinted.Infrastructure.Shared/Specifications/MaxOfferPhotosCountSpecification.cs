using System;
using System.Linq.Expressions;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Specifications
{
    public class MaxOfferPhotosCountSpecification : Specification<Offer>
    {
        private readonly int photosCount;

        private MaxOfferPhotosCountSpecification(int photosCount)
        {
            this.photosCount = photosCount;
        }

        public override Expression<Func<Offer, bool>> ToExpression()
            => offer => offer.OfferPhotos.Count + photosCount <= Constants.MaxFilesCount;

        public static MaxOfferPhotosCountSpecification Create(int photosCount) => new MaxOfferPhotosCountSpecification(photosCount);
    }
}