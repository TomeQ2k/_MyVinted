using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;

namespace MyVinted.API.Controllers
{
    public class OpinionController : BaseController
    {
        public OpinionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOpinion(AddOpinionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} added new opinion #{response.Opinion?.Id} about user #{request.UserId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOpinion([FromQuery] DeleteOpinionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} deleted their opinion #{request.OpinionId} about user #{request.UserId}");

            return this.CreateResponse(response);
        }
    }
}