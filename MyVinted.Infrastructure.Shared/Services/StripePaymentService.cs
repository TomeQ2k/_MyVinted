using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Domain.Entities;
using Stripe;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IUnitOfWork unitOfWork;

        public StripePaymentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<StripePaymentResult> ExecutePayment(PurchaseOrderRequest request, string orderId)
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

            return new StripePaymentResult(charge);
        }

        public async Task<StripeToken> CreatePaymentToken(string tokenId)
        {
            var token = BasePaymentToken.Create<StripeToken>(tokenId);

            unitOfWork.StripeTokenRepository.Add(token);

            return await unitOfWork.Complete() ? token : throw new ServerException("Creating Stripe token failed");
        }
    }
}