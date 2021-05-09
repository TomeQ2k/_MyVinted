using System.Threading.Tasks;

namespace MyVinted.Core.Domain.Data.Repositories.Helpers
{
    public interface IFileDeletable
    {
        Task DeleteFileByPath(string path);
    }
}