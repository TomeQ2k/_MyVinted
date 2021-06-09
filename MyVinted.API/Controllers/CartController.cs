using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCart([FromQuery] GetCartRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their cart");

            return this.CreateResponse(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} added item {request.ProductName} [offer #{request.OfferId}] to their cart");

            return this.CreateResponse(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] RemoveCartItemRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} removed item #{request.ItemId} from their cart");

            return this.CreateResponse(response);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart([FromQuery] ClearCartRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} cleared their cart");

            return this.CreateResponse(response);
        }
    }
}