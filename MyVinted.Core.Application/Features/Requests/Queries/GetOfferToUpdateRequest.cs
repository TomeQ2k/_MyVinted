using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Queries;

namespace MyVinted.Core.Application.Features.Requests.Queries
{
    public record GetOfferToUpdateRequest : IRequest<GetOfferToUpdateResponse>
    {
        public string OfferId { get; init; }
    }

    public class GetOfferToUpdateRequestValidator : AbstractValidator<GetOfferToUpdateRequest>
    {
        public GetOfferToUpdateRequestValidator()
        {
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}