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
using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Application.SignalR.Hubs;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class CreateOfferAuctionCommand : IRequestHandler<CreateOfferAuctionRequest, CreateOfferAuctionResponse>
    {
        private readonly IOfferAuctionManager auctionManager;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public CreateOfferAuctionCommand(IOfferAuctionManager auctionManager, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager)
        {
            this.auctionManager = auctionManager;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<CreateOfferAuctionResponse> Handle(CreateOfferAuctionRequest request, CancellationToken cancellationToken)
        {
            var offerAuction = await auctionManager.CreateAuction(request.NewPrice, request.OfferId);

            if (offerAuction != null)
            {
                var notification = await notifier.Push(NotificationMessages.NewOfferPriceProposedMessage(request.NewPrice, request.OfferId), offerAuction.Offer.OwnerId);
                await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, offerAuction.Offer.OwnerId, mapper.Map<NotificationDto>(notification));

                return new CreateOfferAuctionResponse { OfferAuction = mapper.Map<OfferAuctionDto>(offerAuction) };
            }

            throw new CrudException("Creating offer auction failed");
        }
    }
}