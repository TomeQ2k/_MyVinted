using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class LikeMessageCommand : IRequestHandler<LikeMessageRequest, LikeMessageResponse>
    {
        private readonly IMessenger messenger;

        public LikeMessageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<LikeMessageResponse> Handle(LikeMessageRequest request, CancellationToken cancellationToken)
        {
            var likeMessageResult = await messenger.LikeMessage(request.MessageId);

            return likeMessageResult.IsSucceeded
                ? new LikeMessageResponse {IsLiked = likeMessageResult.IsLiked}
                : throw new ServerException("Changing message like status failed");
        }
    }
}