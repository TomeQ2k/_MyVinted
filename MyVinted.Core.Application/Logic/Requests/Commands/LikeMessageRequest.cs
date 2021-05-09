using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record LikeMessageRequest : IRequest<LikeMessageResponse>
    {
        public string MessageId { get; init; }
    }

    public class LikeMessageRequestValidator : AbstractValidator<LikeMessageRequest>
    {
        public LikeMessageRequestValidator()
        {
            RuleFor(x => x.MessageId).NotNull();
        }
    }
}