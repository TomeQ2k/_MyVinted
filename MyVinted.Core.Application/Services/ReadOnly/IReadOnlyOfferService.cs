using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyOfferService
    {
        Task<Offer> GetOffer(string offerId);
        Task<IPagedList<Offer>> GetOffers(OfferFiltersParams filters);
    }
}