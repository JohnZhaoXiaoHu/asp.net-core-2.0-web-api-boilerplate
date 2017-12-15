namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"E:\Certificates\mlhsales.pfx", "Bx@steel");
        public static string AuthorizationServerBase = "http://120.27.16.7:90";
#endif
    }
}
