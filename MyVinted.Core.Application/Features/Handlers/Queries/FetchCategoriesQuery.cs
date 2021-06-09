using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class FetchCategoriesQuery : IRequestHandler<FetchCategoriesRequest, FetchCategoriesResponse>
    {
        private readonly IReadOnlyCategoryService categoryService;
        private readonly IMapper mapper;

        public FetchCategoriesQuery(IReadOnlyCategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<FetchCategoriesResponse> Handle(FetchCategoriesRequest request, CancellationToken cancellationToken)
            => new FetchCategoriesResponse { Categories = mapper.Map<List<CategoryDto>>(await categoryService.FetchCategories()) };
    }
}