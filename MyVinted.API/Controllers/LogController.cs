using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Common.Helpers;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    [Authorize(Policy = Constants.AdminPolicy)]
    public class LogController : BaseController
    {
        public LogController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched logs");

            return this.CreateResponse(response);
        }
    }
}