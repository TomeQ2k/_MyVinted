using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
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