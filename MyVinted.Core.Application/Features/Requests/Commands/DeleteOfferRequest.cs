using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record DeleteOfferRequest : IRequest<DeleteOfferResponse>
    {
        public string OfferId { get; init; }
    }

    public class DeleteOfferRequestValidator : AbstractValidator<DeleteOfferRequest>
    {
        public DeleteOfferRequestValidator()
        {
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}