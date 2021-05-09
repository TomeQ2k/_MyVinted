using MyVinted.Core.Domain.Data.Repositories.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IOfferPhotoRepository : IRepository<OfferPhoto>, IFileDeletable
    {
        void AddPhoto(string path, string offerId);
    }
}