namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"D:\Projects\test\socialnetwork.pfx", "Bx@steel");
        public static string AuthorizationServerBase = "https://localhost:5000";
#endif
        public static (string Name, string DisplayName) ApiResource = ("authorizationserverapi", "授权中心 APIs");
        public static (string ClientId, string ClientName, string ClientSecret) Client = ("authorizationserver", "授权中心", "authorizationapisecret.abcdefg123456098765~!@#");
    }
}
