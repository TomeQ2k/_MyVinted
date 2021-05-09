using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record AddToCartRequest : IRequest<AddToCartResponse>
    {
        public decimal Amount { get; init; }
        public OrderType Type { get; init; }
        public string ProductName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }

        public string OfferId { get; init; }
        public string OfferOwnerId { get; init; }
    }

    public class AddToCartRequestValidator : AbstractValidator<AddToCartRequest>
    {
        public AddToCartRequestValidator()
        {
            RuleFor(x => x.Amount).NotNull().InclusiveBetween(1, Constants.MaxPrice);
            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.ProductName).NotNull();
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.Email).NotNull().IsEmailAddress();
        }
    }
}