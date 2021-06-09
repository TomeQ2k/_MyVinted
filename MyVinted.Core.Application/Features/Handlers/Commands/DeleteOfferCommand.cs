using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
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