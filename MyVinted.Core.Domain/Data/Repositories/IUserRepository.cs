using System.Threading.Tasks;
using MyVinted.Core.Domain.Data.Models;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IPagedList<User>> GetFilteredUsers(IUserFiltersParams filters, (int PageNumber, int PageSize) pagination);
    }
}