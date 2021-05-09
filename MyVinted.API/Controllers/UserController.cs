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
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] GetUserRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched user #{request.UserId}");

            return this.CreateResponse(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched users");

            return this.CreateResponse(response);
        }

        [HttpPut("follow")]
        public async Task<IActionResult> FollowUser(FollowUserRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} {(response.IsFollowed ? "followed" : "unfollowed")} user #{request.UserId}");

            return this.CreateResponse(response);
        }

        [HttpPatch("block/toggle")]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> ToggleBlockAccount(ToggleBlockAccountRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} {(response.IsBlocked ? "blocked" : "unblocked")} user #{request.UserId}");

            return this.CreateResponse(response);
        }
    }
}