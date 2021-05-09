using MyVinted.Core.Application.Logic.Requests.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(GetUserOrdersRequest request);
        Task<IPagedList<Order>> GetAllOrders(GetAllOrdersRequest request);
    }
}