using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record ChangePasswordRequest : IRequest<ChangePasswordResponse>
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.OldPassword).NotNull();
            RuleFor(x => x.NewPassword).NotNull().Length(Constants.MinPasswordLength, Constants.MaxPasswordLength).NotWhitespaces();
        }
    }
}