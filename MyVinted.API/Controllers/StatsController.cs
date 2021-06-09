using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Common.Helpers;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.Controllers
{
    public class StatsController : BaseController
    {
        public StatsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("premium")]
        [Authorize(Policy = Constants.PremiumPolicy)]
        public async Task<IActionResult> FetchPremiumStats([FromQuery] FetchPremiumStatsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their premium stats");

            return this.CreateResponse(response);
        }

        [HttpGet("admin")]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> FetchAdminStats([FromQuery] FetchAdminStatsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched admin stats");

            return this.CreateResponse(response);
        }
    }
}