using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IPaymentService<TPaymentResult, TPaymentToken>
        where TPaymentResult : PaymentResult
        where TPaymentToken : BasePaymentToken
    {
        Task<TPaymentResult> ExecutePayment(PurchaseOrderRequest request, string orderId);
        Task<TPaymentToken> CreatePaymentToken(string tokenId);
    }
}