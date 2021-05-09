using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;
using Order = MyVinted.Core.Domain.Entities.Order;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyCartManager cartManager;
        private readonly IHttpContextReader httpContextReader;

        public OrderService(IUnitOfWork unitOfWork, IReadOnlyCartManager cartManager, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.cartManager = cartManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<IPagedList<Order>> GetOrders(GetOrdersRequest request)
            => await unitOfWork.OrderRepository.GetFilteredOrdersWithValidatedStatus(request, request.ValidatedStatus, request.Login, (request.PageNumber, request.PageSize));

        public async Task<IEnumerable<Order>> GetUserOrders(GetUserOrdersRequest request)
            => await unitOfWork.OrderRepository.GetFilteredUserValidatedOrders(httpContextReader.CurrentUserId, request);

        public async Task<Order> PurchaseOrder(PurchaseOrderRequest request)
        {
            var cart = await cartManager.GetCart() ?? throw new PaymentException("There are no items to purchase order");
            var token = await CreateStripeToken(request.TokenId) ?? throw new PaymentException("Creating Stripe token failed");
            var orderItems = cart.Items;

            var order = Order.Create(cart.UserId, token.Id);

            unitOfWork.OrderRepository.Add(order);

            if (await ExecuteStripePayment(request, order.Id) == null)
                throw new PaymentException("Stripe payment failed");

            if (!await unitOfWork.Complete())
                throw new PaymentException("Creating order failed");

            orderItems.ToList().ForEach(item =>
            {
                cart.Items.Remove(item);
                order.Items.Add(item);
            });

            cart.User.BookedOffers.ToList().ForEach(o => o.BuyOffer());

            order.CalculateTotalAmount();
            order.Validate();

            unitOfWork.OfferRepository.UpdateRange(cart.User.BookedOffers);
            unitOfWork.CartRepository.Delete(cart);
            unitOfWork.OrderRepository.Update(order);

            return await unitOfWork.Complete() ? order : throw new PaymentException("Creating order failed");
        }

        #region private

        private async Task<StripeToken> CreateStripeToken(string tokenId)
        {
            var token = StripeToken.Create(tokenId);

            unitOfWork.StripeTokenRepository.Add(token);

            return await unitOfWork.Complete() ? token : throw new ServerException("Creating Stripe token failed");
        }

        private async Task<Charge> ExecuteStripePayment(PurchaseOrderRequest request, string orderId)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Source = request.TokenId,
                Amount = (long)(request.TotalAmount * Constants.MoneyMultiplier),
                Currency = request.Currency,
                Description = Constants.OrderMessage(orderId),
                Metadata = new Dictionary<string, string>
                {
                    {"OurRef", $"OurRef-{Utils.NewGuid(length: 32)}"}
                },
                ReceiptEmail = request.Email
            };

            var chargeService = new ChargeService();
            var charge = await chargeService.CreateAsync(chargeOptions);

            return charge;
        }

        #endregion
    }
}