using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record FollowUserRequest : IRequest<FollowUserResponse>
    {
        public string UserId { get; init; }
    }

    public class FollowUserRequestValidator : AbstractValidator<FollowUserRequest>
    {
        public FollowUserRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull();
        }
    }
}