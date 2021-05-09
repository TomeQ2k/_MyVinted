using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record ChangePhoneNumberRequest : IRequest<ChangePhoneNumberResponse>
    {
        public string NewPhoneNumber { get; init; }
    }

    public class ChangePhoneNumberRequestValidator : AbstractValidator<ChangePhoneNumberRequest>
    {
        public ChangePhoneNumberRequestValidator()
        {
            RuleFor(x => x.NewPhoneNumber).NotNull();
        }
    }
}