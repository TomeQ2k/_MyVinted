using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SendChangeEmailCallbackRequest : IRequest<SendChangeEmailCallbackResponse>
    {
        public string NewEmail { get; init; }
    }

    public class SendChangeEmailCallbackRequestValidator : AbstractValidator<SendChangeEmailCallbackRequest>
    {
        public SendChangeEmailCallbackRequestValidator()
        {
            RuleFor(x => x.NewEmail).NotNull().IsEmailAddress();
        }
    }
}