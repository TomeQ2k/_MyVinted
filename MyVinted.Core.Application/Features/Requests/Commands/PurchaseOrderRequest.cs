using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Validation.Validators;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record PurchaseOrderRequest : IRequest<PurchaseOrderResponse>
    {
        public string TokenId { get; init; }
        public decimal TotalAmount { get; init; }
        public string Email { get; init; }
        public string Currency { get; init; } = "usd";
    }

    public class PurchaseOrderRequestValidator : AbstractValidator<PurchaseOrderRequest>
    {
        public PurchaseOrderRequestValidator()
        {
            RuleFor(x => x.TokenId).NotNull();
            RuleFor(x => x.TotalAmount).NotNull();
            RuleFor(x => x.Email).NotNull().IsEmailAddress();
            RuleFor(x => x.Currency).NotNull();
        }
    }
}