using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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