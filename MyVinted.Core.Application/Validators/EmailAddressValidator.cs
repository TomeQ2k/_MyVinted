using FluentValidation;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Helpers;

namespace MyVinted.Core.Application.Validators
{
    public static class EmailAddressValidator
    {
        public static IRuleBuilderOptions<T, TElement> IsEmailAddress<T, TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilder)
            => ruleBuilder.Must(x => (x as string).IsEmailAddress()).WithMessage(ValidatorMessages.EmailAddressValidatorMessage);
    }
}