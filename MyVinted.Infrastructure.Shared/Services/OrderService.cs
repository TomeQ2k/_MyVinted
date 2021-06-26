using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Domain.Data.Models;
using Order = MyVinted.Core.Domain.Entities.Order;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyCartManager cartManager;
        private readonly IStripePaymentService paymentService;
        private readonly IHttpContextReader httpContextReader;

        public OrderService(IUnitOfWork unitOfWork, IReadOnlyCartManager cartManager, IStripePaymentService paymentService, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.cartManager = cartManager;
            this.paymentService = paymentService;
            this.httpContextReader = httpContextReader;
        }

        public async Task<IPagedList<Order>> GetOrders(GetOrdersRequest request)
            => await unitOfWork.OrderRepository.GetFilteredOrdersWithValidatedStatus(request, request.ValidatedStatus, request.Login, (request.PageNumber, request.PageSize));

        public async Task<IEnumerable<Order>> GetUserOrders(GetUserOrdersRequest request)
            => await unitOfWork.OrderRepository.GetFilteredUserValidatedOrders(httpContextReader.CurrentUserId, request);

        public async Task<Order> PurchaseOrder(PurchaseOrderRequest request)
        {
            var cart = await cartManager.GetCart() ?? throw new PaymentException("There are no items to purchase order");
            var token = await paymentService.CreatePaymentToken(request.TokenId) ?? throw new PaymentException("Creating Stripe token failed");
            var orderItems = cart.Items;

            var order = Order.Create(cart.UserId, token.Id);

            unitOfWork.OrderRepository.Add(order);

            if (await paymentService.ExecutePayment(request, order.Id) == null)
                throw new PaymentException("External payment failed");

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
    }
}