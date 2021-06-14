using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using Serilog;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.Controllers
{
    public class MessengerController : BaseController
    {
        public MessengerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations([FromQuery] GetConversationsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their conversations");

            return this.CreateResponse(response);
        }

        [HttpGet("thread")]
        public async Task<IActionResult> GetMessagesThread([FromQuery] GetMessagesThreadRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information(
                $"User #{HttpContext.GetCurrentUserId()} fetched their messages with user #{request.RecipientId}");

            return this.CreateResponse(response);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessageRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} sent message to user #{request.RecipientId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMessage([FromQuery] DeleteMessageRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} deleted message #{request.MessageId}");

            return this.CreateResponse(response);
        }

        [HttpPatch("like")]
        public async Task<IActionResult> LikeMessage(LikeMessageRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information(
                $"User #{HttpContext.GetCurrentUserId()} {(response.IsLiked ? "liked" : "unliked")} message #{request.MessageId}");

            return this.CreateResponse(response);
        }

        [HttpPatch("read")]
        public async Task<IActionResult> ReadMessage(ReadMessageRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} read message #{request.MessageId}");

            return this.CreateResponse(response);
        }

        [HttpGet("unread/count")]
        public async Task<IActionResult> CountUnreadMessages([FromQuery] CountUnreadMessagesRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} counted their unread messages");

            return this.CreateResponse(response);
        }
    }
}