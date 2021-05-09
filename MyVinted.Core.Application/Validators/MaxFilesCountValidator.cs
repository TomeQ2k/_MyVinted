using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Helpers;
using System.Collections.Generic;

namespace MyVinted.Core.Application.Validators
{
    public static class MaxFilesCountValidator
    {
        public static IRuleBuilderOptions<T, TElement> MaxFilesCountIs<T, TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilder, int maxFilesCount)
            => ruleBuilder.Must(files => (files as List<IFormFile>) == null ? true : (files as List<IFormFile>).Count <= maxFilesCount).WithMessage(ValidatorMessages.MaxFilesCountValidatorMessage(maxFilesCount));
    }
}