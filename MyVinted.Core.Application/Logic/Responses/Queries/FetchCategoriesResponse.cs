using System.Collections.Generic;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Queries
{
    public record FetchCategoriesResponse : BaseResponse
    {
        public IEnumerable<CategoryDto> Categories { get; init; }

        public FetchCategoriesResponse(Error error = null) : base(error) { }
    }
}