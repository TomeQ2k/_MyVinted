using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record SendMessageRequest : IRequest<SendMessageResponse>
    {
        public string Text { get; init; }
        public string RecipientId { get; init; }
    }

    public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageRequestValidator()
        {
            RuleFor(x => x.Text).NotNull().MaximumLength(Constants.MaxMessageLength);
            RuleFor(x => x.RecipientId).NotNull();
        }
    }
}