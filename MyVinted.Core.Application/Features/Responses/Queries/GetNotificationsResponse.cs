using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Features.Responses.Queries
{
    public record GetNotificationsResponse : BaseResponse
    {
        public IEnumerable<NotificationDto> Notifications { get; init; }

        public GetNotificationsResponse(Error error = null) : base(error) { }
    }
}