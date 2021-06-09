using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class DeleteMessageCommand : IRequestHandler<DeleteMessageRequest, DeleteMessageResponse>
    {
        private readonly IMessenger messenger;

        public DeleteMessageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<DeleteMessageResponse> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
            => await messenger.DeleteMessage(request.MessageId) ? new DeleteMessageResponse()
                : throw new CrudException("Deleting message failed");
    }
}