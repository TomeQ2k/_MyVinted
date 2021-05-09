using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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