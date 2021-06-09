using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record RemoveNotificationRequest : IRequest<RemoveNotificationResponse>
    {
        public string NotificationId { get; init; }
    }

    public class RemoveNotificationRequestValidator : AbstractValidator<RemoveNotificationRequest>
    {
        public RemoveNotificationRequestValidator()
        {
            RuleFor(x => x.NotificationId).NotNull();
        }
    }
}