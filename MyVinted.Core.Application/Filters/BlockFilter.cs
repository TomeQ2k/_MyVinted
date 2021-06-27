using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Domain.Data;

namespace MyVinted.Core.Application.Filters
{
    public class BlockFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext result;

            string currentUserId = context.HttpContext.GetCurrentUserId();

            if (currentUserId == null)
            {
                result = await next();
                return;
            }

            var database = context.HttpContext.RequestServices.GetService<IUnitOfWork>();

            var currentUser = await database.UserRepository.FindById(currentUserId) ?? throw new EntityNotFoundException("User not found");

            if (currentUser.IsBlocked)
                throw new BlockException();

            result = await next();
        }
    }
}