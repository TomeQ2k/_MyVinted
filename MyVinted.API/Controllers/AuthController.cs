using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User {request.Email} signed in");

            return this.CreateResponse(response);
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User {request.Email} signed up");

            return this.CreateResponse(response);
        }

        [HttpGet("signUp/confirm")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] ConfirmAccountRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User {request.Email} confirmed their account");

            return this.CreateResponse(response);
        }

        [HttpGet("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User {request.Email} resetted their password");

            return this.CreateResponse(response);
        }

        [HttpPost("resetPassword/send")]
        public async Task<IActionResult> SendResetPasswordCallback(SendResetPasswordCallbackRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User {request.Email} sent reset password link to email address: {request.Email}");

            return this.CreateResponse(response);
        }

        [HttpPost("signIn/external")]
        public async Task<IActionResult> SignInWithExternalProvider(SignInWithExternalProviderRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"External user signed in using provider: {request.Provider}");

            return this.CreateResponse(response);
        }
    }
}