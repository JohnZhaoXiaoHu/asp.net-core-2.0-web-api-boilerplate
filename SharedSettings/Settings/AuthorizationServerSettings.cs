namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"C:\Certificates\mlhsales.pfx", "Bx@steel");
        public static string AuthorizationServerBase = "http://cgzl.me:80";
#endif
    }
}
