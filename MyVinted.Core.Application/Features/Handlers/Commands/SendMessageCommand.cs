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
    public class SendMessageCommand : IRequestHandler<SendMessageRequest, SendMessageResponse>
    {
        private readonly IMessenger messenger;
        private readonly IMapper mapper;
        private readonly IHubManager<MessengerHub> hubManager;

        public SendMessageCommand(IMessenger messenger, IMapper mapper, IHubManager<MessengerHub> hubManager)
        {
            this.messenger = messenger;
            this.mapper = mapper;
            this.hubManager = hubManager;
        }

        public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var messageSent = await messenger.SendMessage(request.Text, request.RecipientId);

            if (messageSent != null)
            {
                await hubManager.Invoke(SignalrActions.MESSAGE_RECEIVED, request.RecipientId, mapper.Map<MessageDto>(messageSent));

                return new SendMessageResponse { Message = mapper.Map<MessageDto>(messageSent) };
            }

            throw new CrudException("Sending message failed");
        }
    }
}