using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    public class OfferController : BaseController
    {
        public OfferController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetOffer([FromQuery] GetOfferRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched offer #{request.OfferId}");

            return this.CreateResponse(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetOffers([FromQuery] GetOffersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched offers");

            return this.CreateResponse(response);
        }

        [HttpGet("my/filter")]
        public async Task<IActionResult> GetMyOffers([FromQuery] GetMyOffersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their offers");

            return this.CreateResponse(response);
        }

        [HttpGet("update")]
        public async Task<IActionResult> GetOfferToUpdate([FromQuery] GetOfferToUpdateRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched offer #{request.OfferId} to update");

            return this.CreateResponse(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOffer([FromForm] AddOfferRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} created new offer #{response.Offer?.Id}");

            return this.CreateResponse(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOffer([FromForm] UpdateOfferRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} updated their offer #{request.OfferId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOffer([FromQuery] DeleteOfferRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} deleted their offer #{request.OfferId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("photos/delete")]
        public async Task<IActionResult> DeleteOfferPhoto([FromQuery] DeleteOfferPhotoRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} deleted offer photo #{request.PhotoId} in offer #{request.OfferId}");

            return this.CreateResponse(response);
        }
    }
}