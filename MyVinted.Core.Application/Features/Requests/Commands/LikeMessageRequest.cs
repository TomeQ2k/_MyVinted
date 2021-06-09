using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
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