using MyVinted.Core.Application.Results;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IStripePaymentService : IPaymentService<StripePaymentResult, StripeToken>
    {
    }
}