using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record DenyOfferAuctionRequest : IRequest<DenyOfferAuctionResponse>
    {
        public string AuctionId { get; init; }
        public string OfferId { get; init; }
    }

    public class DenyOfferAuctionRequestValidator : AbstractValidator<DenyOfferAuctionRequest>
    {
        public DenyOfferAuctionRequestValidator()
        {
            RuleFor(x => x.AuctionId).NotNull();
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}