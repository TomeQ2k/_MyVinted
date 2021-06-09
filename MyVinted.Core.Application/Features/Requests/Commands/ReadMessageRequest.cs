using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record ReadMessageRequest : IRequest<ReadMessageResponse>
    {
        public string MessageId { get; init; }
    }

    public class ReadMessageRequestValidator : AbstractValidator<ReadMessageRequest>
    {
        public ReadMessageRequestValidator()
        {
            RuleFor(x => x.MessageId).NotNull();
        }
    }
}