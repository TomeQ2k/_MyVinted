using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Validation.Validators;

namespace MyVinted.Core.Application.Features.Requests.Commands
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