using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Validation
{
    public class ValidatorBehavior : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new ValidationFailedResult(context.ModelState, Error.Build(ErrorCodes.ValidationError, ValidatorMessages.DefaultValidatorMessage,
                    HttpStatusCode.UnprocessableEntity));
        }
    }
}