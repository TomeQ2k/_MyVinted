using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    public class AuctionController : BaseController
    {
        public AuctionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOfferAuction(CreateOfferAuctionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} created offer #{request.OfferId} auction with new price: {request.NewPrice} $");

            return this.CreateResponse(response);
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptOfferAuction(AcceptOfferAuctionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} accepted offer #{request.OfferId} auction #{request.AuctionId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("deny")]
        public async Task<IActionResult> DenyOfferAuction([FromQuery] DenyOfferAuctionRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} denied offer #{request.OfferId} auction #{request.AuctionId}");

            return this.CreateResponse(response);
        }
    }
}