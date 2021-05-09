using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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