namespace MyVinted.Core.Common.Helpers
{
    public static class AppSettingsKeys
    {
        #region constants

        public const string ConnectionString = "DatabaseConnectionString";
        public const string ServerAddress = "Constants:ServerAddress";
        public const string ClientAddress = "Constants:ClientAddress";
        public const string Token = "Constants:Token";
        public const string TLSToken = "Constants:TLSToken";
        public const string ClientId = "ClientId";
        public const string ClientSecret = "ClientSecret";
        public const string AppId = "AppId";
        public const string AppSecret = "AppSecret";
        public const string SecretKey = "SecretKey";

        #endregion

        #region sections

        public const string GoogleAuthSection = "ExternalAuthentication:Google";
        public const string FacebookAuthSection = "ExternalAuthentication:Facebook";
        public const string IpRateLimitingSection = "IpRateLimiting";
        public const string StripeSection = "Stripe";

        #endregion
    }
}