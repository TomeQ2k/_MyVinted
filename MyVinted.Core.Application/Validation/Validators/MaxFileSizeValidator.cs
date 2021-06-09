using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Validation.Validators
{
    public static class MaxFileSizeValidator
    {
        public static IRuleBuilderOptions<T, TElement> MaxFileSizeIs<T, TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilder, int maxFileSize, bool isCollection = false)
            => ruleBuilder.Must(upload => isCollection switch
            {
                false when upload != null && upload is IFormFile => (upload as IFormFile).Length <= maxFileSize * Constants.UnitConversionMultiplier * Constants.UnitConversionMultiplier,
                true when upload != null && upload is List<IFormFile> => (upload as List<IFormFile>)
                    .All(f => f.Length <= maxFileSize * Constants.UnitConversionMultiplier * Constants.UnitConversionMultiplier),
                _ => true
            }).WithMessage(ValidatorMessages.MaxFileSizeValidatorMessage(maxFileSize));
    }
}