using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record ChangeEmailRequest : IRequest<ChangeEmailResponse>
    {
        public string NewEmail { get; init; }
        public string Token { get; init; }
    }

    public class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
    {
        public ChangeEmailRequestValidator()
        {
            RuleFor(x => x.NewEmail).NotNull().IsEmailAddress();
            RuleFor(x => x.Token).NotNull();
        }
    }
}