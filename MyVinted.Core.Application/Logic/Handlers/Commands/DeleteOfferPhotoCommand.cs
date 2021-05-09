using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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