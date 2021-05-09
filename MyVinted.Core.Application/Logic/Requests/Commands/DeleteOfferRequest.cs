using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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