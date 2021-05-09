using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record AddOpinionRequest : IRequest<AddOpinionResponse>
    {
        public string Text { get; init; }
        public int Rating { get; init; }
        public string UserId { get; init; }
    }

    public class AddOpinionRequestValidator : AbstractValidator<AddOpinionRequest>
    {
        public AddOpinionRequestValidator()
        {
            RuleFor(x => x.Text).NotNull().MaximumLength(Constants.MaxDescriptionLength);
            RuleFor(x => x.Rating).NotNull().InclusiveBetween(1, 5);
            RuleFor(x => x.UserId).NotNull();
        }
    }
}