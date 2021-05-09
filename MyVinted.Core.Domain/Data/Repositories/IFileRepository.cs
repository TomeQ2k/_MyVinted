using MyVinted.Core.Domain.Data.Repositories.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IFileRepository : IRepository<File>, IFileDeletable
    {
        void AddFile(string path);
    }
}