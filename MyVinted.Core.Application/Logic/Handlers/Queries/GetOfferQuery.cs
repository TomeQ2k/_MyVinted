using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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