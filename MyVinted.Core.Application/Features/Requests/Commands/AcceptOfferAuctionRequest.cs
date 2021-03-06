using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record AcceptOfferAuctionRequest : IRequest<AcceptOfferAuctionResponse>
    {
        public string AuctionId { get; init; }
        public string OfferId { get; init; }
    }

    public class AcceptOfferAuctionRequestValidator : AbstractValidator<AcceptOfferAuctionRequest>
    {
        public AcceptOfferAuctionRequestValidator()
        {
            RuleFor(x => x.AuctionId).NotNull();
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}