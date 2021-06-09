using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ReadMessageCommand : IRequestHandler<ReadMessageRequest, ReadMessageResponse>
    {
        private readonly IMessenger messenger;

        public ReadMessageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<ReadMessageResponse> Handle(ReadMessageRequest request, CancellationToken cancellationToken)
            => await messenger.ReadMessage(request.MessageId) ? new ReadMessageResponse()
                : throw new CrudException("Reading message failed");
    }
}