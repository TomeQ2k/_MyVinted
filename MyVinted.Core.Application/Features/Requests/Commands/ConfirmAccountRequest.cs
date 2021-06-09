using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record ConfirmAccountRequest : IRequest<ConfirmAccountResponse>
    {
        public string Email { get; init; }
        public string Token { get; init; }
    }

    public class ConfirmAccountRequestValidator : AbstractValidator<ConfirmAccountRequest>
    {
        public ConfirmAccountRequestValidator()
        {
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Token).NotNull();
        }
    }
}