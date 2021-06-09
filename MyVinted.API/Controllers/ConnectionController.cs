using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;

namespace MyVinted.API.Controllers
{
    public class ConnectionController : BaseController
    {
        public ConnectionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartConnection(StartConnectionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} started their connection #{request.ConnectionId} in hub: {request.HubName}");

            return this.CreateResponse(response);
        }

        [HttpDelete("close")]
        [AllowAnonymous]
        public async Task<IActionResult> CloseConnection([FromQuery] CloseConnectionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} closed their connection in hub: {request.HubName}");

            return this.CreateResponse(response);
        }
    }
}