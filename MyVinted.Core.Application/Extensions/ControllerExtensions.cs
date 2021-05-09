using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Logic.Responses;

namespace MyVinted.Core.Application.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult CreateResponse(this ControllerBase controller, IBaseResponse response)
            => response.IsSucceeded ? controller.Ok(response) : controller.StatusCode((int)response.Error.StatusCode, response);
    }
}