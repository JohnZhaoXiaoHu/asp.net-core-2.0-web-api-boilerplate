namespace SalesApi.Shared.Settings
{
    public class PurchaseClientSettings
    {
#if DEBUG
        public const string ClientUriBase = "http://localhost:3000";
#else
        public const string ClientUriBase = "http://120.27.16.7:93";
#endif
        public static string CorsPolicyName = "purchase";
        public static string CorsOrigin = ClientUriBase;
        public static (string ClientId, string ClientName, string RedirectUri, string SilentRedirectUri, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("purchase", "采购系统", $"{ClientUriBase}/others/login-callback.html", $"{ClientUriBase}/silent-renew.html", $"{ClientUriBase}", $"{ClientUriBase}");
    }
}
