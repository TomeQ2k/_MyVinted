using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record LikeOfferRequest : IRequest<LikeOfferResponse>
    {
        public string OfferId { get; init; }
    }

    public class LikeOfferRequestValidator : AbstractValidator<LikeOfferRequest>
    {
        public LikeOfferRequestValidator()
        {
            RuleFor(x => x.OfferId).NotEmpty();
        }
    }
}