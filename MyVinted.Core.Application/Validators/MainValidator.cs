using Microsoft.AspNetCore.Mvc.Filters;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Validators
{
    public class MainValidator : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState, Error.Build(ErrorCodes.ValidationError, ValidatorMessages.MainValidatorMessage));
        }
    }
}