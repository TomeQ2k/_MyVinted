using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Queries.Params;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyFavoritesService
    {
        Task<IPagedList<Offer>> GetFavoritesOffers(OfferFiltersParams filters);
    }
}