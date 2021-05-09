using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SignUpRequest : IRequest<SignUpResponse>
    {
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().IsEmailAddress();
            RuleFor(x => x.Username).NotNull().Length(Constants.MinUsernameLength, Constants.MaxUsernameLength).NotWhitespaces();
            RuleFor(x => x.Password).NotNull().Length(Constants.MinPasswordLength, Constants.MaxPasswordLength).NotWhitespaces();
        }
    }
}