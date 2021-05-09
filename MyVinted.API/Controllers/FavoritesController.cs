using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    public class FavoritesController : BaseController
    {
        public FavoritesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFavoritesOffers([FromQuery] GetFavoritesOffersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their favorites offers");

            return this.CreateResponse(response);
        }

        [HttpPut("like")]
        public async Task<IActionResult> LikeOffer(LikeOfferRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} added offer #{request.OfferId} to their favorites");

            return this.CreateResponse(response);
        }
    }
}