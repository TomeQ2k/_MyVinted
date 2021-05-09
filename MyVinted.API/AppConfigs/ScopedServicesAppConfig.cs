using Microsoft.Extensions.DependencyInjection;
using MyVinted.API.BackgroundServices;
using MyVinted.API.BackgroundServices.Interfaces;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Domain.Data;
using MyVinted.Infrastructure.Persistence.Database;
using MyVinted.Infrastructure.Shared.Services;
using MyVinted.Infrastructure.Shared.SignalR;

namespace MyVinted.API.AppConfigs
{
    public static class ScopedServicesAppConfig
    {
        public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IGoogleIdentityService, GoogleIdentityService>();
            services.AddScoped<IFacebookIdentityService, FacebookIdentityService>();
            services.AddScoped<IResetPasswordManager, ResetPasswordManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IRolesManager, RolesManager>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IOpinionService, OpinionService>();
            services.AddScoped<IMessenger, Messenger>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IOfferAuctionManager, OfferAuctionManager>();
            services.AddScoped<IJwtAuthorizationTokenGenerator, JwtAuthorizationTokenGenerator>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped(typeof(IHubManager<>), typeof(HubManager<>));
            services.AddScoped<IConnectionManager, ConnectionManager>();
            services.AddScoped<IAuthValidationService, AuthValidationService>();
            services.AddScoped<INotifierValidationService, NotifierValidationService>();
            services.AddScoped<IOfferAuctionValidationService, OfferAuctionValidationService>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPremiumStatsService, PremiumStatsService>();
            services.AddScoped<IAdminStatsService, AdminStatsService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IBookingCleaner, BookingCleaner>();
            services.AddScoped<IStripeTokenCleaner, StripeTokenCleaner>();
            services.AddScoped<IBalanceValidationService, BalanceValidationService>();

            services.AddScoped<IReadOnlyAccountManager, AccountManager>();
            services.AddScoped<IReadOnlyRolesManager, RolesManager>();
            services.AddScoped<IReadOnlyOfferService, OfferService>();
            services.AddScoped<IReadOnlyCategoryService, CategoryService>();
            services.AddScoped<IReadOnlyFavoritesService, FavoritesService>();
            services.AddScoped<IReadOnlyUserService, UserService>();
            services.AddScoped<IReadOnlyNotifier, Notifier>();
            services.AddScoped<IReadOnlyMessenger, Messenger>();
            services.AddScoped<IReadOnlyCartManager, CartManager>();
            services.AddScoped<IReadOnlyOrderService, OrderService>();

            return services;
        }
    }
}