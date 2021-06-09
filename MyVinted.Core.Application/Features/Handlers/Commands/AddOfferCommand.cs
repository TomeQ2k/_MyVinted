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
using MyVinted.Core.Application.ServiceUtils;
using MyVinted.Core.Application.SignalR;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class AddOfferCommand : IRequestHandler<AddOfferRequest, AddOfferResponse>
    {
        private readonly IOfferService offerService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public AddOfferCommand(IOfferService offerService, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager)
        {
            this.offerService = offerService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<AddOfferResponse> Handle(AddOfferRequest request, CancellationToken cancellationToken)
        {
            var createdOffer = await offerService.AddOffer(request);

            if (createdOffer != null)
            {
                var followersIds = FollowersUtils.GetFollowersIds(createdOffer.Owner);

                foreach (var followerId in followersIds)
                {
                    var notification = await notifier.Push(NotificationMessages.NewOfferByFollowedUserAddedMessage(createdOffer.Owner.UserName), followerId);
                    await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, followerId, mapper.Map<NotificationDto>(notification));
                }

                return new AddOfferResponse { Offer = mapper.Map<OfferDto>(createdOffer) };
            }

            throw new CrudException("Adding offer failed");
        }
    }
}