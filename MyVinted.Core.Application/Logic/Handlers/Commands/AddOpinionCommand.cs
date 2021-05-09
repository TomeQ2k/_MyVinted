using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.ServiceUtils;
using MyVinted.Core.Application.SignalR;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class AddOpinionCommand : IRequestHandler<AddOpinionRequest, AddOpinionResponse>
    {
        private readonly IOpinionService opinionService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHubManager<NotifierHub> hubManager;

        public AddOpinionCommand(IOpinionService opinionService, IMapper mapper, INotifier notifier, IHubManager<NotifierHub> hubManager)
        {
            this.opinionService = opinionService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<AddOpinionResponse> Handle(AddOpinionRequest request, CancellationToken cancellationToken)
        {
            var opinionAdded = await opinionService.AddOpinion(request.Text, request.Rating, request.UserId);

            if (opinionAdded != null)
            {
                var notification = await notifier.Push(NotificationMessages.NewOpinionAdded(opinionAdded.Creator.UserName), opinionAdded.UserId);
                await hubManager.Invoke(SignalrActions.NOTIFICATION_RECEIVED, opinionAdded.UserId, mapper.Map<NotificationDto>(notification));

                return new AddOpinionResponse { Opinion = mapper.Map<OpinionDto>(opinionAdded), NewRating = RatingUtils.CalculateRating(opinionAdded.User) };
            }

            throw new CrudException("Adding opinion failed");
        }
    }
}