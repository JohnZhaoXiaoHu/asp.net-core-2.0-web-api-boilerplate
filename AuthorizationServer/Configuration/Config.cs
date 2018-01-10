using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityModel;
using SalesApi.Shared.Settings;

namespace AuthorizationServer.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(SalesApiSettings.ApiName, SalesApiSettings.ApiDisplayName) {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName, JwtClaimTypes.Email }
                },
                new ApiResource("purchaseapi", "采购和原料库API") {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName, JwtClaimTypes.Email }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // Sales JavaScript Client
                new Client
                {
                    ClientId = SalesApiSettings.ClientId,
                    ClientName = SalesApiSettings.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60 * 10,
                    AllowOfflineAccess = true,
                    RedirectUris =           { $"{Startup.Configuration["MLH:SalesApi:ClientBase"]}/login-callback", $"{Startup.Configuration["MLH:SalesApi:ClientBase"]}/silent-renew.html" },
                    PostLogoutRedirectUris = { Startup.Configuration["MLH:SalesApi:ClientBase"] },
                    AllowedCorsOrigins =     { Startup.Configuration["MLH:SalesApi:ClientBase"] },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        SalesApiSettings.ApiName
                    }
                },
                // Purchase JavaScript Client
                new Client
                {
                    ClientId = "purchase",
                    ClientName = "采购和原料库",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60 * 10,
                    AllowOfflineAccess = true,
                    RedirectUris =           { $"{Startup.Configuration["MLH:PurchaseApi:ClientBase"]}/others/login-callback.html", $"{Startup.Configuration["MLH:PurchaseApi:ClientBase"]}/others/silent-renew.html" },
                    PostLogoutRedirectUris = { Startup.Configuration["MLH:PurchaseApi:ClientBase"] },
                    AllowedCorsOrigins =     { Startup.Configuration["MLH:PurchaseApi:ClientBase"] },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "purchaseapi"
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
