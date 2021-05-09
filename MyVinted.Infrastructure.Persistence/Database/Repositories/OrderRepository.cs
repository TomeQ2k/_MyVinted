using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.SmartEnums;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data.Models;
using MyVinted.Core.Domain.Data.Repositories;
using MyVinted.Core.Domain.Data.Repositories.Params;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Persistence.Database.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetFilteredUserValidatedOrders(string userId, IOrderFiltersParams filters)
        {
            var orders = context.Orders.Where(o => o.UserId == userId && o.IsValidated);

            orders = OrderSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(orders);

            return await orders.ToListAsync();
        }

        public async Task<IPagedList<Order>> GetFilteredOrdersWithValidatedStatus(IOrderFiltersParams filters, OrderValidatedStatus validatedStatus, string login, (int PageNumber, int PageSize) pagination)
        {
            var orders = context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(login))
                orders = orders.Where(o => o.User.UserName.ToLower().Contains(login.ToLower()) || o.User.Email.ToLower().Contains(login.ToLower()));

            orders = OrderValidatedStatusSmartEnum.FromValue((int)validatedStatus).Filter(orders);

            orders = OrderSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(orders);

            return await orders.ToPagedList<Order>(pagination.PageNumber, pagination.PageSize);
        }
    }
}