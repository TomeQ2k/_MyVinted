using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class UpdateOfferCommand : IRequestHandler<UpdateOfferRequest, UpdateOfferResponse>
    {
        private readonly IOfferService offerService;
        private readonly IMapper mapper;

        public UpdateOfferCommand(IOfferService offerService, IMapper mapper)
        {
            this.offerService = offerService;
            this.mapper = mapper;
        }

        public async Task<UpdateOfferResponse> Handle(UpdateOfferRequest request, CancellationToken cancellationToken)
        {
            var offerToUpdate = await offerService.GetOffer(request.OfferId);
            offerToUpdate = mapper.Map<UpdateOfferRequest, Offer>(request, offerToUpdate);

            return await offerService.UpdateOffer(offerToUpdate, request.Photos)
                ? new UpdateOfferResponse()
                : throw new CrudException("Updating offer failed");
        }
    }
}