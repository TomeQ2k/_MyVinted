using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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