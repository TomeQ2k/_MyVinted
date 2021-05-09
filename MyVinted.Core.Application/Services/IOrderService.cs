using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IOrderService : IReadOnlyOrderService
    {
        Task<Order> PurchaseOrder(PurchaseOrderRequest request);
    }
}