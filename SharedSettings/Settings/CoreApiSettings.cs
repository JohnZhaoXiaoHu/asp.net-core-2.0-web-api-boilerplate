using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSettings.Settings
{
    public class CoreApiSettings
    {
        #region CoreApi
        
        public static string CorsPolicyName = "default";
        public static string CorsOrigin = "http://localhost:4200";
        public static (string Name, string DisplayName) ApiResource = ("coreapi", "核心系统 API");
        public static (string ClientId, string ClientName, string RedirectUri, string SilentRedirectUri, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("core", "核心系统", "http://localhost:4200/login-callback", "http://localhost:4200/silent-renew.html", "http://localhost:4200/index.html", "http://localhost:4200");

        #endregion
    }
}
