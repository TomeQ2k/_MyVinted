using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery] GetProfileRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their profile data");

            return this.CreateResponse(response);
        }

        [HttpPatch("changeUsername")]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} changed their username to: {request.NewUsername}");

            return this.CreateResponse(response);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} changed their password");

            return this.CreateResponse(response);
        }

        [HttpGet("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromQuery] ChangeEmailRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} changed their email address to: {request.NewEmail}");

            return this.CreateResponse(response);
        }

        [HttpPost("changeEmail/send")]
        public async Task<IActionResult> SendChangeEmailCallback(SendChangeEmailCallbackRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} sent change email link to email address: {request.NewEmail}");

            return this.CreateResponse(response);
        }

        [HttpPatch("changePhoneNumber")]
        public async Task<IActionResult> ChangePhoneNumber(ChangePhoneNumberRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} changed their phone number to: {request.NewPhoneNumber}");

            return this.CreateResponse(response);
        }

        [HttpPost("avatar/set")]
        public async Task<IActionResult> SetAvatar([FromForm] SetAvatarRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} set their avatar photo");

            return this.CreateResponse(response);
        }

        [HttpDelete("avatar/delete")]
        public async Task<IActionResult> DeleteAvatar([FromForm] DeleteAvatarRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} deleted their avatar photo");

            return this.CreateResponse(response);
        }
    }
}