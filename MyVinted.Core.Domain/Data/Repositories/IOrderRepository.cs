using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Models;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetFilteredUserValidatedOrders(string userId, IOrderFiltersParams filters);

        Task<IPagedList<Order>> GetFilteredOrdersWithValidatedStatus(IOrderFiltersParams filters, OrderValidatedStatus validatedStatus, string login, (int PageNumber, int PageSize) pagination);
    }
}