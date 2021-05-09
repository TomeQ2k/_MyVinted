using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record CreateOfferAuctionRequest : IRequest<CreateOfferAuctionResponse>
    {
        public decimal NewPrice { get; init; }
        public string OfferId { get; init; }
    }

    public class CreateOfferAuctionRequestValidator : AbstractValidator<CreateOfferAuctionRequest>
    {
        public CreateOfferAuctionRequestValidator()
        {
            RuleFor(x => x.NewPrice).NotNull().InclusiveBetween(1, Constants.MaxPrice);
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}