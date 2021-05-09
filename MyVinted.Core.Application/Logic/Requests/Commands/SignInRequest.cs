using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SignInRequest : IRequest<SignInResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}