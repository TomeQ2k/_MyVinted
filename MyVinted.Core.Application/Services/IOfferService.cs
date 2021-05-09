using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IOfferService : IReadOnlyOfferService
    {
        Task<Offer> AddOffer(AddOfferRequest request);
        Task<bool> UpdateOffer(Offer offerToUpdate, IEnumerable<IFormFile> photos = null);
        Task<bool> DeleteOffer(string offerId);

        Task<bool> DeletePhoto(string photoId, string offerId);
    }
}