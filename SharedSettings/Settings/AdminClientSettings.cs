namespace SharedSettings.Settings
{
    public class AdminClientSettings
    {
        public static string CorsPolicyName = "default";
        public static string CorsOrigin = "http://localhost:4200";
        public static (string Name, string DisplayName) ApiResource = ("adminapi", "后台管理 APIs");
        public static (string ClientId, string ClientName, string RedirectUris, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("admin", "后台管理系统", "http://localhost:4200/login-callback", "http://localhost:4200/index.html", "http://localhost:4200");
    }
}
