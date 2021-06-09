using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record ToggleBlockAccountRequest : IRequest<ToggleBlockAccountResponse>
    {
        public string UserId { get; init; }
    }

    public class ToggleBlockAccountRequestValidator : AbstractValidator<ToggleBlockAccountRequest>
    {
        public ToggleBlockAccountRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull();
        }
    }
}