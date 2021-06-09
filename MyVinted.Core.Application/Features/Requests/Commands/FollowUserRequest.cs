using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
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