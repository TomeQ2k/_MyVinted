using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Common.Helpers
{
    public static class Constants
    {
        #region values

        public const int MinPasswordLength = 5;
        public const int MaxPasswordLength = 30;
        public const int MinUsernameLength = 5;
        public const int MaxUsernameLength = 24;

        public const int MaxTitleLength = 150;
        public const int MaxDescriptionLength = 1500;
        public const int MaxMessageLength = 300;
        public const decimal MaxPrice = 5000;

        public const int MaxFilesCount = 5;
        public const int MaxFileSize = 3;

        public const int UnitConversionMultiplier = 1024;
        public const int MoneyMultiplier = 100;

        public const int BookingHostedServiceTimeInMinutes = 4320;
        public const int StripeTokenHostedServiceTimeInMinutes = 10080;

        public const int DefaultAccountBalance = 200;

        public const string LogFilesPath = "./wwwroot/logs/log-.txt";

        public const int PageSize = 10;
        public const int LogsPageSize = 50;

        #endregion

        #region policies

        public const string PremiumPolicy = "PremiumPolicy";
        public const string AdminPolicy = "AdminPolicy";

        public const string CorsPolicy = "CorsPolicy";

        #endregion

        #region roles

        public const string PremiumRole = "Premium";
        public const string AdminRole = "Admin";
        public static RoleName[] RolesToSeed = { RoleName.User, RoleName.Premium, RoleName.ExternalUser, RoleName.Admin };

        #endregion

        #region uris

        public const string FacebookUri = "https://graph.facebook.com/v2.8/";

        #endregion

        #region externalProviders

        public const string GoogleProvider = "GOOGLE";
        public const string FacebookProvider = "FACEBOOK";

        #endregion

        #region payments

        public static string OrderMessage(string orderId) => $"Order #{orderId} has been completed";

        #endregion
    }
}