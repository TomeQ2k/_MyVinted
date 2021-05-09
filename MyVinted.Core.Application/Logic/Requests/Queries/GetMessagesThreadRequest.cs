using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using MyVinted.Core.Application.Logic.Responses.Queries;

namespace MyVinted.Core.Application.Logic.Requests.Queries
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