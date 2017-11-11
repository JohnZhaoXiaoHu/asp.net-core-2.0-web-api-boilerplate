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
        public static (string Name, string DisplayName) ApiResource = ("salesapi", "销售系统 APIs");
        public static (string ClientId, string ClientName, string RedirectUris, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("sales", "销售系统", "http://localhost:4200/login-callback", "http://localhost:4200/index.html", "http://localhost:4200");

        #endregion
    }
}
