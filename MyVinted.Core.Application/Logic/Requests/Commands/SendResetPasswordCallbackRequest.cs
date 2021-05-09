using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SendResetPasswordCallbackRequest : IRequest<SendResetPasswordCallbackResponse>
    {
        public string Email { get; init; }
        public string NewPassword { get; init; }
    }

    public class SendResetPasswordCallbackRequestValidator : AbstractValidator<SendResetPasswordCallbackRequest>
    {
        public SendResetPasswordCallbackRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().IsEmailAddress();
            RuleFor(x => x.NewPassword).NotNull().Length(Constants.MinPasswordLength, Constants.MaxPasswordLength).NotWhitespaces();
        }
    }
}