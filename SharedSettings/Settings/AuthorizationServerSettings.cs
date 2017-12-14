namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"D:\Certificates\mlhsales.pfx", "Bx@steel");
        public static string AuthorizationServerBase = "https://10.13.1.159:5000";
#endif
    }
}
