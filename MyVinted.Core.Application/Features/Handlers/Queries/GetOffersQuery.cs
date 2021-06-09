using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetOffersQuery : IRequestHandler<GetOffersRequest, GetOffersResponse>
    {
        private readonly IReadOnlyOfferService offerService;
        private readonly IMapper mapper;
        private readonly IHttpContextWriter httpContextWriter;

        public GetOffersQuery(IReadOnlyOfferService offerService, IMapper mapper, IHttpContextWriter httpContextWriter)
        {
            this.offerService = offerService;
            this.mapper = mapper;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetOffersResponse> Handle(GetOffersRequest request, CancellationToken cancellationToken)
        {
            var offers = await offerService.GetOffers(request);

            var offersToReturn = mapper.Map<List<OfferListDto>>(offers);

            httpContextWriter.AddPagination(offers.CurrentPage, offers.PageSize, offers.TotalCount, offers.TotalPages);

            return new GetOffersResponse {Offers = offersToReturn};
        }
    }
}