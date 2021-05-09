using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record RemoveCartItemRequest : IRequest<RemoveCartItemResponse>
    {
        public string ItemId { get; init; }
    }

    public class RemoveCartItemRequestValidator : AbstractValidator<RemoveCartItemRequest>
    {
        public RemoveCartItemRequestValidator()
        {
            RuleFor(x => x.ItemId).NotNull();
        }
    }
}