using MyVinted.Core.Application.SmartEnums;
using MyVinted.Core.Domain.Data.Repositories;
using System.Linq;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;
using System.Threading.Tasks;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Domain.Data.Models;
using System.Collections.Generic;
using System;
using MyVinted.Core.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Offer>> GetFilteredOffers(IOfferFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var offers = !filters.OnlyVerified ? context.Offers : context.Offers.Where(o => o.IsVerified);

            if (!string.IsNullOrEmpty(filters.Title))
                offers = offers.Where(o => o.Title.ToLower().Contains(filters.Title.ToLower()));

            if (filters.CategoryId != null)
                offers = offers.Where(o => o.CategoryId == filters.CategoryId);

            if (filters.UserId != null)
                offers = offers.Where(o => o.OwnerId == filters.UserId);

            offers = BoughtOfferStatusSmartEnum.FromValue((int)filters.BoughtOfferStatus).Filter(offers);
            offers = OfferSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(offers);

            return await offers.ToPagedList<Offer>(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IPagedList<Offer>> GetFavoritesFilteredOffers(IOfferFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var offers = !filters.OnlyVerified ? context.Offers : context.Offers.Where(o => o.IsVerified);

            if (!string.IsNullOrEmpty(filters.Title))
                offers = offers.Where(o => o.Title.ToLower().Contains(filters.Title.ToLower()));

            if (filters.CategoryId != null)
                offers = offers.Where(o => o.CategoryId == filters.CategoryId);

            offers = offers.Where(o => o.OfferLikes.Any(l => l.UserId == filters.UserId));

            offers = BoughtOfferStatusSmartEnum.FromValue((int)filters.BoughtOfferStatus).Filter(offers);
            offers = OfferSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(offers);

            return await offers.ToPagedList<Offer>(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<Offer>> GetExpiredBookedOffers()
            => await context.Offers
                .Where(o => o.BookingUserId != null
                    && o.BookingUser.Cart != null && o.BookingUser.Cart.Items.Any(i => i.DateCreated.AddMinutes(Constants.BookingHostedServiceTimeInMinutes) < DateTime.Now
                    && i.OrderId == null))
                .ToListAsync();

        public void UpdateOffer(Offer offer)
        {
            offer.UpdateDate();
            Update(offer);
        }
    }
}