namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"E:\Certificates\my.pfx", "Password");
        public static string AuthorizationServerBase = "http://localhost:90";
#endif
    }
}
