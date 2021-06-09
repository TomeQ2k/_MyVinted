using MyVinted.Core.Application.Builders;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;
        private readonly IBookingService bookingService;

        public CartManager(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager,
            IBookingService bookingService)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
            this.bookingService = bookingService;
        }

        public async Task<Cart> GetCart() => (await accountManager.GetCurrentUser())?.Cart;

        public async Task<OrderItem> AddItem(AddToCartRequest request)
        {
            var currentUser = await accountManager.GetCurrentUser();

            if (currentUser.Id == request.OfferOwnerId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            if (currentUser.Cart == null)
                currentUser.SetCart(await CreateCart(currentUser));

            var item = new OrderItemBuilder()
                .SetAmount(request.Amount)
                .SetType(request.Type)
                .SetProductName(request.ProductName)
                .SetUserName(request.UserName)
                .SetEmail(request.Email)
                .SetCartId(currentUser.Cart.Id)
                .SetOptionalData(request.OfferOwnerId)
                .Build();

            currentUser.Cart.Items.Add(item);

            currentUser.Cart.CalculateTotalAmount();

            return await unitOfWork.Complete() ? item : throw new ServerException("Adding item to cart failed");
        }

        public async Task<bool> RemoveItem(string itemId)
        {
            var cart = await GetCart() ?? throw new EntityNotFoundException("Cart not found");
            var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == itemId) ??
                               throw new EntityNotFoundException("Cart item not found");

            if (itemToRemove.Type == OrderType.Offer)
                await bookingService.CancelBooking(cart);

            cart.Items.Remove(itemToRemove);

            await DeleteCartIfEmpty(cart);

            unitOfWork.OrderItemRepository.Delete(itemToRemove);

            cart.CalculateTotalAmount();

            return await unitOfWork.Complete();
        }

        public async Task<bool> ClearCart()
        {
            var cart = await GetCart() ?? throw new EntityNotFoundException("Cart not found");
            var itemsToRemove = cart.Items;

            foreach (var itemToRemove in itemsToRemove)
                if (itemToRemove.Type == OrderType.Offer)
                    await bookingService.CancelBooking(cart);

            unitOfWork.OrderItemRepository.DeleteRange(itemsToRemove);
            unitOfWork.CartRepository.Delete(cart);

            return await unitOfWork.Complete();
        }

        #region private

        private async Task<Cart> CreateCart(User currentUser)
        {
            if (currentUser.Cart != null)
                throw new DuplicateException("You already have your cart created");

            var cart = Cart.Create(currentUser.Id);

            unitOfWork.CartRepository.Add(cart);

            return await unitOfWork.Complete() ? cart : throw new ServerException("Creating cart failed");
        }

        private async Task<bool> DeleteCartIfEmpty(Cart cart)
        {
            if (cart.Items.Any())
                return false;

            unitOfWork.CartRepository.Delete(cart);

            return await unitOfWork.Complete();
        }

        #endregion
    }
}