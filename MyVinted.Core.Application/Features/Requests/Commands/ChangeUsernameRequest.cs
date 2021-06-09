using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Validation.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record ChangeUsernameRequest : IRequest<ChangeUsernameResponse>
    {
        public string NewUsername { get; init; }
    }

    public class ChangeUsernameRequestValidator : AbstractValidator<ChangeUsernameRequest>
    {
        public ChangeUsernameRequestValidator()
        {
            RuleFor(x => x.NewUsername).NotNull().Length(Constants.MinUsernameLength, Constants.MaxUsernameLength).NotWhitespaces();
        }
    }
}