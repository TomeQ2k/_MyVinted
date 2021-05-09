using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class DeleteOfferCommand : IRequestHandler<DeleteOfferRequest, DeleteOfferResponse>
    {
        private readonly IOfferService offerService;

        public DeleteOfferCommand(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        public async Task<DeleteOfferResponse> Handle(DeleteOfferRequest request, CancellationToken cancellationToken)
            => await offerService.DeleteOffer(request.OfferId)
                ? new DeleteOfferResponse()
                : throw new CrudException("Deleting offer failed");
    }
}