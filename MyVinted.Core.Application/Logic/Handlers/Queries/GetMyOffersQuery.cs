using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
{
    public class GetMyOffersQuery : IRequestHandler<GetMyOffersRequest, GetMyOffersResponse>
    {
        private readonly IReadOnlyOfferService offerService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public GetMyOffersQuery(IReadOnlyOfferService offerService, IMapper mapper,
            IHttpContextService httpContextService)
        {
            this.offerService = offerService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<GetMyOffersResponse> Handle(GetMyOffersRequest request, CancellationToken cancellationToken)
        {
            var offers = await offerService.GetOffers(request with {UserId = httpContextService.CurrentUserId});

            var offersToReturn = mapper.Map<List<OfferListDto>>(offers);

            httpContextService.AddPagination(offers.CurrentPage, offers.PageSize,
                offers.TotalCount, offers.TotalPages);

            return new GetMyOffersResponse {Offers = offersToReturn};
        }
    }
}