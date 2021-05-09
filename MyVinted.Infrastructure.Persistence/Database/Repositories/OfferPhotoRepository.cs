using MyVinted.Core.Domain.Data.Repositories;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class OfferPhotoRepository : Repository<OfferPhoto>, IOfferPhotoRepository
    {
        public OfferPhotoRepository(DataContext context) : base(context)
        {
        }

        public void AddPhoto(string path, string offerId)
        {
            var photoToAdd = OfferPhoto.Create(path, offerId);

            Add(photoToAdd);
        }

        public async Task DeleteFileByPath(string path)
        {
            var photoToDelete = await Find(p => p.Path.ToLower().Contains(path.ToLower()));

            if (photoToDelete != null)
                Delete(photoToDelete);
        }
    }
}