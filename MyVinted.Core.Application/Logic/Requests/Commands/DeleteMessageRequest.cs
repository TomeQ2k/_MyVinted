using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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