using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetOfferToUpdateQuery : IRequestHandler<GetOfferToUpdateRequest, GetOfferToUpdateResponse>
    {
        private readonly IReadOnlyOfferService offerService;
        private readonly IMapper mapper;

        public GetOfferToUpdateQuery(IReadOnlyOfferService offerService, IMapper mapper)
        {
            this.offerService = offerService;
            this.mapper = mapper;
        }

        public async Task<GetOfferToUpdateResponse> Handle(GetOfferToUpdateRequest request, CancellationToken cancellationToken)
        {
            var offerToUpdate = await offerService.GetOffer(request.OfferId);

            return offerToUpdate != null ? new GetOfferToUpdateResponse { OfferToUpdate = mapper.Map<OfferToUpdateDto>(offerToUpdate) }
                : throw new EntityNotFoundException("Offer not found");
        }
    }
}