using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Queries;

namespace MyVinted.Core.Application.Logic.Requests.Queries
{
    public record GetOfferRequest : IRequest<GetOfferResponse>
    {
        public string OfferId { get; init; }
    }

    public class GetOfferRequestValidator : AbstractValidator<GetOfferRequest>
    {
        public GetOfferRequestValidator()
        {
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}