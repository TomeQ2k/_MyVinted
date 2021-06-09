using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record SignInRequest : IRequest<SignInResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}