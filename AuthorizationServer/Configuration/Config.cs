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
                new ApiResource(SalesApiSettings.ApiResource.Name, SalesApiSettings.ApiResource.DisplayName) {
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
                    ClientId = SalesApiSettings.Client.ClientId,
                    ClientName = SalesApiSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60 * 10,
                    AllowOfflineAccess = true,
                    RedirectUris =           { SalesApiSettings.Client.RedirectUri, SalesApiSettings.Client.SilentRedirectUri },
                    PostLogoutRedirectUris = { SalesApiSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { SalesApiSettings.Client.AllowedCorsOrigins },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        SalesApiSettings.ApiResource.Name
                    }
                },
                // Purchase JavaScript Client
                new Client
                {
                    ClientId = PurchaseClientSettings.Client.ClientId,
                    ClientName = PurchaseClientSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60 * 10,
                    AllowOfflineAccess = true,
                    RedirectUris =           { PurchaseClientSettings.Client.RedirectUri, PurchaseClientSettings.Client.SilentRedirectUri },
                    PostLogoutRedirectUris = { PurchaseClientSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { PurchaseClientSettings.Client.AllowedCorsOrigins },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        SalesApiSettings.ApiResource.Name
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
