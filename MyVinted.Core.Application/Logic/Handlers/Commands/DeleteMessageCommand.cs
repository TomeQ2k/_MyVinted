using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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