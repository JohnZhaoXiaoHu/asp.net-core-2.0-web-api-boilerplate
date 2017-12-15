namespace SalesApi.Shared.Settings
{
    public class SalesApiSettings
    {
#if DEBUG
        public const string SalesApiServerBase = "http://localhost:5100";
        public const string ClientUriBase = "http://localhost:4200";
#else
        public const string SalesApiServerBase = "http://localhost:91";
        public const string ClientUriBase = "http://localhost:92";
#endif
        public static string CorsPolicyName = "sales";
        public static string CorsOrigin = ClientUriBase;
        public static (string Name, string DisplayName) ApiResource = ("salesapi", "销售系统 APIs");
        public static (string ClientId, string ClientName, string RedirectUri, string SilentRedirectUri, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("sales", "销售系统", $"{ClientUriBase}/login-callback", $"{ClientUriBase}/silent-renew.html", $"{ClientUriBase}", $"{ClientUriBase}");
    }
}
