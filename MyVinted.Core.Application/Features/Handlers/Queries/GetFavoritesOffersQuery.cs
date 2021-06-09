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
    public class GetFavoritesOffersQuery : IRequestHandler<GetFavoritesOffersRequest, GetFavoritesOffersResponse>
    {
        private readonly IReadOnlyFavoritesService favoritesService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public GetFavoritesOffersQuery(IReadOnlyFavoritesService favoritesService, IMapper mapper,
            IHttpContextService httpContextService)
        {
            this.favoritesService = favoritesService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<GetFavoritesOffersResponse> Handle(GetFavoritesOffersRequest request,
            CancellationToken cancellationToken)
        {
            var favoritesOffers = await favoritesService.GetFavoritesOffers(request with
            {
                UserId = httpContextService.CurrentUserId
            });

            var favoritesOffersToReturn = mapper.Map<List<OfferListDto>>(favoritesOffers);

            httpContextService.AddPagination(favoritesOffers.CurrentPage, favoritesOffers.PageSize,
                favoritesOffers.TotalCount, favoritesOffers.TotalPages);

            return new GetFavoritesOffersResponse
            {
                FavoritesOffers = favoritesOffersToReturn
            };
        }
    }
}