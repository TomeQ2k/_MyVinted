using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Application.SignalR.Hubs;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class FollowUserCommand : IRequestHandler<FollowUserRequest, FollowUserResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public FollowUserCommand(IUserService userService, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<FollowUserResponse> Handle(FollowUserRequest request, CancellationToken cancellationToken)
        {
            var (isFollowed, follow) = await userService.FollowUser(request.UserId);

            if (isFollowed)
            {
                var notification = await notifier.Push(NotificationMessages.UserFollowMessage(follow.Follower.UserName), request.UserId);
                await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, request.UserId, mapper.Map<NotificationDto>(notification));
            }

            return new FollowUserResponse { IsFollowed = isFollowed, Follow = mapper.Map<UserFollowDto>(follow) };
        }
    }
}