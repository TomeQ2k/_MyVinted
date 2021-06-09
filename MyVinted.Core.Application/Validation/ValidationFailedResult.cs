using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyVinted.Core.Application.Features.Responses;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState, Error error)
            : base(new ValidationResponse(modelState, error))
                => (StatusCode) = (StatusCodes.Status422UnprocessableEntity);
    }
}