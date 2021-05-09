using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Queries;

namespace MyVinted.Core.Application.Logic.Requests.Queries
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