using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IFavoritesService : IReadOnlyFavoritesService
    {
        Task<(bool, OfferLike)> LikeOffer(string offerId);
    }
}