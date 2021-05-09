using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface ICategoryService : IReadOnlyCategoryService
    {
        Task<bool> InsertCategoriesFromFile();
    }
}