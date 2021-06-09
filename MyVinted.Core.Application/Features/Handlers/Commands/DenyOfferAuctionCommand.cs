using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Application.ServiceUtils;
using MyVinted.Core.Application.SignalR;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class DenyOfferAuctionCommand : IRequestHandler<DenyOfferAuctionRequest, DenyOfferAuctionResponse>
    {
        private readonly IOfferAuctionManager auctionManager;
        private readonly IReadOnlyOfferService offerService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public DenyOfferAuctionCommand(IOfferAuctionManager auctionManager, IReadOnlyOfferService offerService, IMapper mapper, INotifier notifier,
            IHubManager<NotifierHub> hubManager)
        {
            this.auctionManager = auctionManager;
            this.offerService = offerService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<DenyOfferAuctionResponse> Handle(DenyOfferAuctionRequest request, CancellationToken cancellationToken)
        {
            var offer = await offerService.GetOffer(request.OfferId) ?? throw new EntityNotFoundException("Offer not found");

            var denied = await auctionManager.DenyAuction(request.AuctionId, request.OfferId);

            if (denied)
            {
                var followersIds = FollowersUtils.GetFollowersIds(offer.Owner);

                foreach (var followerId in followersIds)
                {
                    var notification = await notifier.Push(NotificationMessages.OfferAuctionDenied(request.OfferId), followerId);
                    await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, followerId, mapper.Map<NotificationDto>(notification));
                }

                return new DenyOfferAuctionResponse();
            }

            throw new CrudException("Deny offer auction failed");
        }
    }
}