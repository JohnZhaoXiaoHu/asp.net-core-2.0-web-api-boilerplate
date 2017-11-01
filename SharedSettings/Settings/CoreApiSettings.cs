using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSettings.Settings
{
    public class CoreApiSettings
    {
        #region CoreApi

        public static string AuthorizationServerBase = "http://localhost:5000";
        public static string CorsPolicyName = "default";
        public static string CorsOrigin = "http://localhost:4200";
        public static (string Name, string DisplayName) CoreApiResource = ("coreapi", "Core APIs");
        public static (string ClientId, string ClientName, string RedirectUris, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("corejs", "Core Javascript Client", "http://localhost:4200/login-callback", "http://localhost:4200/index.html", "http://localhost:4200");

        #endregion
    }
}
