using FluentValidation;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Helpers;

namespace MyVinted.Core.Application.Validation.Validators
{
    public static class WhitespacesNotAllowedValidator
    {
        public static IRuleBuilderOptions<T, TElement> NotWhitespaces<T, TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilder)
            => ruleBuilder.Must(x => !(x as string).HasWhitespaces()).WithMessage(ValidatorMessages.WhitespacesNotAllowedValidatorMessage);
    }
}