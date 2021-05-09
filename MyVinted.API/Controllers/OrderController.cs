using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Common.Helpers;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("filter")]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched admin orders history");

            return this.CreateResponse(response);
        }

        [HttpGet("user/filter")]
        public async Task<IActionResult> GetUserOrders([FromQuery] GetUserOrdersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their orders history");

            return this.CreateResponse(response);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseOrder(PurchaseOrderRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} purchased order #{response.Order?.Id} for {(decimal)request.TotalAmount / Constants.MoneyMultiplier} {request.Currency}");

            return this.CreateResponse(response);
        }
    }
}