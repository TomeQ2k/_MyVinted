using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Requests.Queries.Params;
using MyVinted.Core.Application.Features.Responses.Queries;

namespace MyVinted.Core.Application.Features.Requests.Queries
{
    public record GetMessagesThreadRequest : MessengerFiltersParams, IRequest<GetMessagesThreadResponse>
    {
        public string RecipientId { get; init; }
    }

    public class GetMessagesThreadRequestValidator : AbstractValidator<GetMessagesThreadRequest>
    {
        public GetMessagesThreadRequestValidator()
        {
            RuleFor(x => x.RecipientId).NotNull();
        }
    }
}