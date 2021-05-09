using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SignInWithExternalProviderRequest : IRequest<SignInWithExternalProviderResponse>
    {
        public string Provider { get; init; }
        public string IdToken { get; init; }
    }

    public class SignInWithExternalProviderRequestValidator : AbstractValidator<SignInWithExternalProviderRequest>
    {
        public SignInWithExternalProviderRequestValidator()
        {
            RuleFor(x => x.Provider).NotNull();
            RuleFor(x => x.IdToken).NotNull();
        }
    }
}