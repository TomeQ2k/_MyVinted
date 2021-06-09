using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Queries;

namespace MyVinted.Core.Application.Features.Requests.Queries
{
    public record GetUserRequest : IRequest<GetUserResponse>
    {
        public string UserId { get; init; }
    }

    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull();
        }
    }
}