using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Data.Models;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Task<IPagedList<Offer>> GetFilteredOffers(IOfferFiltersParams filters, (int PageNumber, int PageSize) pagination);
        Task<IPagedList<Offer>> GetFavoritesFilteredOffers(IOfferFiltersParams filters, (int PageNumber, int PageSize) pagination);
        
        Task<IEnumerable<Offer>> GetExpiredBookedOffers();

        void UpdateOffer(Offer offer);
    }
}