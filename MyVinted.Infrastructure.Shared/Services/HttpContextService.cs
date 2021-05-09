using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Services;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserId => httpContextAccessor.HttpContext.GetCurrentUserId();

        public void AddPagination(int currentPage, int pageSize, int totalCount, int totalPages)
            => httpContextAccessor.HttpContext.Response.AddPagination(currentPage, pageSize, totalCount, totalPages);
    }
}