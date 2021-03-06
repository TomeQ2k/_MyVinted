using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;

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

            Log.Information(
                $"User #{HttpContext.GetCurrentUserId()} {(response.IsLiked ? "liked" : "unliked")} offer #{request.OfferId}");

            return this.CreateResponse(response);
        }
    }
}