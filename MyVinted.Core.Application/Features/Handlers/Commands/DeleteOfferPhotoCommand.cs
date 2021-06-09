using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class DeleteOfferPhotoCommand : IRequestHandler<DeleteOfferPhotoRequest, DeleteOfferPhotoResponse>
    {
        private readonly IOfferService offerService;

        public DeleteOfferPhotoCommand(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        public async Task<DeleteOfferPhotoResponse> Handle(DeleteOfferPhotoRequest request, CancellationToken cancellationToken)
            => await offerService.DeletePhoto(request.PhotoId, request.OfferId)
                ? new DeleteOfferPhotoResponse()
                : throw new DeleteFileException("Deleting offer photo failed");
    }
}