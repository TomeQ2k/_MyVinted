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
    public class GetOfferQuery : IRequestHandler<GetOfferRequest, GetOfferResponse>
    {
        private readonly IReadOnlyOfferService offerService;
        private readonly IMapper mapper;

        public GetOfferQuery(IReadOnlyOfferService offerService, IMapper mapper)
        {
            this.offerService = offerService;
            this.mapper = mapper;
        }

        public async Task<GetOfferResponse> Handle(GetOfferRequest request, CancellationToken cancellationToken)
        {
            var offer = await offerService.GetOffer(request.OfferId);

            return offer != null ? new GetOfferResponse { Offer = mapper.Map<OfferDto>(offer) }
                : throw new EntityNotFoundException("Offer not found");
        }
    }
}