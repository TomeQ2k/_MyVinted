using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> FetchCategories([FromQuery] FetchCategoriesRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched {response.Categories.Count()} categories");

            return this.CreateResponse(response);
        }
    }
}