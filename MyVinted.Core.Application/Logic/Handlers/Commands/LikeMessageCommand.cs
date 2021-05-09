using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class LikeMessageCommand : IRequestHandler<LikeMessageRequest, LikeMessageResponse>
    {
        private readonly IMessenger messenger;

        public LikeMessageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<LikeMessageResponse> Handle(LikeMessageRequest request, CancellationToken cancellationToken)
            => await messenger.LikeMessage(request.MessageId) ? new LikeMessageResponse()
                : throw new CrudException("Liking message failed");
    }
}