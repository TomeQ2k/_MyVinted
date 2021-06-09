using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record StartConnectionRequest : IRequest<StartConnectionResponse>
    {
        public string ConnectionId { get; init; }
        public string HubName { get; init; }
    }

    public class StartConnectionRequestValidator : AbstractValidator<StartConnectionRequest>
    {
        public StartConnectionRequestValidator()
        {
            RuleFor(x => x.ConnectionId).NotNull();
            RuleFor(x => x.HubName).NotNull();
        }
    }
}