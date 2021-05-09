using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Entities;
using MyVinted.Infrastructure.Persistence.Database;

namespace MyVinted.API.AppConfigs
{
    public static class IdentityAppConfig
    {
        public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
            => services.AddIdentityCore<User>(options =>
               {
                   options.Password.RequiredLength = Constants.MinPasswordLength;
                   options.Password.RequireDigit = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
               })
               .AddRoles<Role>()
               .AddRoleManager<RoleManager<Role>>()
               .AddSignInManager<SignInManager<User>>()
               .AddRoleValidator<RoleValidator<Role>>()
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();
    }
}