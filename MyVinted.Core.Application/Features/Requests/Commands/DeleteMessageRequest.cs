using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record DeleteMessageRequest : IRequest<DeleteMessageResponse>
    {
        public string MessageId { get; init; }
    }

    public class DeleteMessageRequestValidator : AbstractValidator<DeleteMessageRequest>
    {
        public DeleteMessageRequestValidator()
        {
            RuleFor(x => x.MessageId).NotNull();
        }
    }
}