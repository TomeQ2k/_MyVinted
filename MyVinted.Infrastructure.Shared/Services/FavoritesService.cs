using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Queries.Params;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;

        public FavoritesService(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
        }

        public async Task<IPagedList<Offer>> GetFavoritesOffers(OfferFiltersParams filters)
            => await unitOfWork.OfferRepository.GetFavoritesFilteredOffers(filters, (filters.PageNumber, filters.PageSize));

        public async Task<(bool, OfferLike)> LikeOffer(string offerId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var offer = await unitOfWork.OfferRepository.Get(offerId) ??
                        throw new EntityNotFoundException("Offer not found");

            if (currentUser.Id == offer.OwnerId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            var like = currentUser.OfferLikes.FirstOrDefault(o => o.OfferId == offerId);

            if (like != null)
            {
                unitOfWork.OfferLikeRepository.Delete(like);

                return await unitOfWork.Complete()
                    ? (false, null)
                    : throw new ServerException("Deleting offer like failed");
            }

            like = OfferLike.Create(offerId, currentUser.Id);

            offer.OfferLikes.Add(like);
            currentUser.OfferLikes.Add(like);

            return await unitOfWork.Complete()
                ? (true, like)
                : throw new ServerException("Adding offer like failed");
        }
    }
}