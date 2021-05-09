using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record CloseConnectionRequest : IRequest<CloseConnectionResponse>
    {
        public string HubName { get; init; }
    }

    public class CloseConnectionRequestValidator : AbstractValidator<CloseConnectionRequest>
    {
        public CloseConnectionRequestValidator()
        {
            RuleFor(x => x.HubName).NotNull();
        }
    }
}