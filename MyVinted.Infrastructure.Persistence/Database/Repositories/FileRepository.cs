using MyVinted.Core.Domain.Data.Repositories;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context)
        {
        }

        public void AddFile(string path)
        {
            var fileToAdd = BaseFile.Create<File>(path);

            Add(fileToAdd);
        }

        public async Task DeleteFileByPath(string path)
        {
            var fileToDelete = await Find(f => f.Path.ToLower().Contains(path.ToLower()));

            if (fileToDelete != null)
                Delete(fileToDelete);
        }
    }
}