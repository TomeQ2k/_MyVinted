using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Validators;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record ResetPasswordRequest : IRequest<ResetPasswordResponse>
    {
        public string Email { get; init; }
        public string NewPassword { get; init; }
        public string Token { get; init; }
    }

    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().IsEmailAddress();
            RuleFor(x => x.NewPassword).NotNull();
            RuleFor(x => x.Token).NotNull();
        }
    }
}