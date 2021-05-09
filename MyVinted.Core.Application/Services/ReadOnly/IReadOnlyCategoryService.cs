using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyCategoryService
    {
        Task<IEnumerable<Category>> FetchCategories();
    }
}