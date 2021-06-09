using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record MarkAsReadNotificationRequest : IRequest<MarkAsReadNotificationResponse>
    {
        public string NotificationId { get; init; }
    }

    public class MarkAsReadNotificationRequestValidator : AbstractValidator<MarkAsReadNotificationRequest>
    {
        public MarkAsReadNotificationRequestValidator()
        {
            RuleFor(x => x.NotificationId).NotNull();
        }
    }
}