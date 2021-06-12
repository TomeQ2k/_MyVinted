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
    public class LikeOfferCommand : IRequestHandler<LikeOfferRequest, LikeOfferResponse>
    {
        private readonly IFavoritesService favoritesService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public LikeOfferCommand(IFavoritesService favoritesService, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager)
        {
            this.favoritesService = favoritesService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<LikeOfferResponse> Handle(LikeOfferRequest request, CancellationToken cancellationToken)
        {
            var (isLiked, like) = await favoritesService.LikeOffer(request.OfferId);

            if (isLiked)
            {
                var notification = await notifier.Push(NotificationMessages.OfferFollowMessage(like.User.UserName, request.OfferId), like.Offer.OwnerId);
                await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, like.Offer.OwnerId, mapper.Map<NotificationDto>(notification));
            }

            return new LikeOfferResponse { IsLiked = isLiked, Like = mapper.Map<OfferLikeDto>(like) };
        }
    }
}