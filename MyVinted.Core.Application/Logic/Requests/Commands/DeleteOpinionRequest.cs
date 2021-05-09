using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record DeleteOpinionRequest : IRequest<DeleteOpinionResponse>
    {
        public string OpinionId { get; init; }
        public string UserId { get; init; }
    }

    public class DeleteOpinionRequestValidator : AbstractValidator<DeleteOpinionRequest>
    {
        public DeleteOpinionRequestValidator()
        {
            RuleFor(x => x.OpinionId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}