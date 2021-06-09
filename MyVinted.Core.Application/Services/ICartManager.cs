using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface ICartManager : IReadOnlyCartManager
    {
        Task<OrderItem> AddItem(AddToCartRequest request);

        Task<bool> RemoveItem(string itemId);
        Task<bool> ClearCart();
    }
}