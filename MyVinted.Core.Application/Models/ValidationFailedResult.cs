using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyVinted.Core.Application.Logic.Responses;

namespace MyVinted.Core.Application.Models
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState, Error error)
            : base(new ValidationResponse(modelState, error))
                => (StatusCode) = (StatusCodes.Status422UnprocessableEntity);
    }
}