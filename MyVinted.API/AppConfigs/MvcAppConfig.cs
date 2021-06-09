using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Filters;
using MyVinted.Core.Application.Validators;
using System.Text.Json;

namespace MyVinted.API.AppConfigs
{
    public static class MvcAppConfig
    {
        public static IMvcBuilder ConfigureMvc(this IServiceCollection services)
            => services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidatorBehavior));
                options.Filters.Add(typeof(BlockFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddMvcOptions(options => options.EnableEndpointRouting = false)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
    }
}