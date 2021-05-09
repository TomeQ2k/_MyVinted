using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class PurchaseOrderCommand : IRequestHandler<PurchaseOrderRequest, PurchaseOrderResponse>
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;
        private readonly IBalanceService balanceService;
        private readonly IRolesManager rolesManager;
        private readonly IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator;
        private readonly IBalanceValidationService balanceValidationService;
        private readonly IHttpContextReader httpContextReader;

        public PurchaseOrderCommand(IOrderService orderService, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager,
            IBalanceService balanceService, IRolesManager rolesManager, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IBalanceValidationService balanceValidationService,
            IHttpContextReader httpContextReader)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
            this.balanceService = balanceService;
            this.rolesManager = rolesManager;
            this.jwtAuthorizationTokenGenerator = jwtAuthorizationTokenGenerator;
            this.balanceValidationService = balanceValidationService;
            this.httpContextReader = httpContextReader;
        }

        public async Task<PurchaseOrderResponse> Handle(PurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            if (!await balanceValidationService.HasEnoughFunds(httpContextReader.CurrentUserId, request.TotalAmount))
                throw new PaymentException("Insufficient funds to complete transaction");

            var order = await orderService.PurchaseOrder(request);

            var orderOfferItems = order.Items.Where(i => i.Type == OrderType.Offer);
            var offersData = orderOfferItems.Select(i => new { OwnerId = i.OptionalData, Amount = (decimal)i.Amount / Constants.MoneyMultiplier });

            foreach (var item in orderOfferItems)
            {
                var notification = await notifier.Push(NotificationMessages.OfferBoughtMessage(order.User.UserName, item.ProductName), item.OptionalData);
                await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, item.OptionalData, mapper.Map<NotificationDto>(notification));
            }

            var premiumOrder = order.Items.FirstOrDefault(i => i.Type == OrderType.Premium);

            if (premiumOrder != null && !await rolesManager.AdmitRole(RoleName.Premium, order.User))
                throw new PaymentException("Upgrading account to premium status failed");

            if (order != null)
            {
                foreach (var offerData in offersData)
                {
                    await balanceService.AddBalance(offerData.OwnerId, offerData.Amount);
                    await balanceService.AddBalance(order.UserId, -offerData.Amount);
                }

                string token = default(string);

                if (premiumOrder != null)
                {
                    await balanceService.AddBalance(order.UserId, -(decimal)premiumOrder.Amount / Constants.MoneyMultiplier);
                    token = await jwtAuthorizationTokenGenerator.GenerateToken(order.User);
                }

                return new PurchaseOrderResponse { Order = mapper.Map<OrderDto>(order), Token = token };
            }

            throw new PaymentException("Purchasing order failed");
        }
    }
}