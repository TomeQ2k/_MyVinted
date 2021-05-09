using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using Serilog;
using System.Threading.Tasks;

namespace MyVinted.API.Controllers
{
    public class NotificationController : BaseController
    {
        public NotificationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched their notification");

            return this.CreateResponse(response);
        }

        [HttpPatch("read")]
        public async Task<IActionResult> MarkAsRead(MarkAsReadNotificationRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} marked notification #{request.NotificationId} as read");

            return this.CreateResponse(response);
        }

        [HttpPut("readAll")]
        public async Task<IActionResult> MarkAllAsRead(MarkAllAsReadNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} marked all their notifications as read");

            return this.CreateResponse(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveNotification([FromQuery] RemoveNotificationRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} removed notification #{request.NotificationId}");

            return this.CreateResponse(response);
        }

        [HttpDelete("clearAll")]
        public async Task<IActionResult> ClearAllNotifications([FromQuery] ClearAllNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} cleared all their notifications");

            return this.CreateResponse(response);
        }

        [HttpGet("unread/count")]
        public async Task<IActionResult> CountUnreadNotifications([FromQuery] CountUnreadNotificationsRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} counted their unread notifications");

            return this.CreateResponse(response);
        }
    }
}