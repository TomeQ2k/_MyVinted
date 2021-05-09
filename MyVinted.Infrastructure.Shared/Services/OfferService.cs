using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Builders;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;
        private readonly IFilesManager filesManager;
        private readonly IHttpContextReader httpContextReader;

        public OfferService(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager, IFilesManager filesManager,
            IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
            this.filesManager = filesManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<Offer> GetOffer(string offerId)
            => await unitOfWork.OfferRepository.Get(offerId) ?? throw new EntityNotFoundException("Offer not found");

        public async Task<IPagedList<Offer>> GetOffers(OfferFiltersParams filters)
            => await unitOfWork.OfferRepository.GetFilteredOffers(filters, (filters.PageNumber, filters.PageSize));

        public async Task<Offer> AddOffer(AddOfferRequest request)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var category = await unitOfWork.CategoryRepository.Get(request.CategoryId) ??
                           throw new EntityNotFoundException("Category not found");

            var offer = new OfferBuilder()
                .SetTitle(request.Title)
                .SetPrice(request.Price)
                .SetDescription(request.Description)
                .AllowBidding(request.AllowBidding)
                .Build();

            if (currentUser.IsVerified())
                offer.Verify();

            currentUser.Offers.Add(offer);
            category.Offers.Add(offer);

            if (request.Photos != null)
                await UploadOfferPhotos(request.Photos, offer.Id);

            return await unitOfWork.Complete() ? offer : null;
        }

        public async Task<bool> UpdateOffer(Offer offerToUpdate, IEnumerable<IFormFile> photos = null)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthException(ErrorMessages.NotAuthenticatedMessage);

            if (offerToUpdate.OwnerId != currentUserId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            if (offerToUpdate.Owner.IsVerified())
                offerToUpdate.Verify();

            if (photos != null)
            {
                if (photos.Count() + offerToUpdate.OfferPhotos.Count <= Constants.MaxFilesCount)
                    await UploadOfferPhotos(photos, offerToUpdate.Id);
                else
                    throw new UploadFileException($"Maximum files count is: {Constants.MaxFilesCount}");
            }

            unitOfWork.OfferRepository.UpdateOffer(offerToUpdate);

            return await unitOfWork.Complete();
        }

        public async Task<bool> DeletePhoto(string photoId, string offerId)
        {
            var offerPhoto = await unitOfWork.OfferPhotoRepository.Get(photoId) ??
                             throw new EntityNotFoundException("Offer photo not found");

            if (offerPhoto.OfferId != offerId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            if (offerPhoto.Offer.OfferPhotos.Count - 1 == 0)
                throw new DeleteFileException("At least one photo is required");

            unitOfWork.OfferPhotoRepository.Delete(offerPhoto);

            filesManager.Delete(offerPhoto.Path);

            return await unitOfWork.Complete();
        }

        public async Task<bool> DeleteOffer(string offerId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var offer = currentUser.Offers.FirstOrDefault(o => o.Id == offerId) ??
                        throw new EntityNotFoundException("Offer not found");

            unitOfWork.OfferRepository.Delete(offer);

            if (!await unitOfWork.Complete())
                return false;

            filesManager.DeleteDirectory($"files/offers/{offer.Id}");

            return true;
        }

        #region private

        private async Task UploadOfferPhotos(IEnumerable<IFormFile> photos, string offerId)
        {
            foreach (var photo in photos)
            {
                var uploadedPhoto = await filesManager.Upload(photo, $"offers/{offerId}");
                unitOfWork.OfferPhotoRepository.AddPhoto(uploadedPhoto?.Path, offerId);
            }
        }

        #endregion
    }
}