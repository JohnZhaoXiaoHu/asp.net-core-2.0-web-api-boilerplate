using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using SharedSettings.Settings;

namespace AuthorizationServer.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(CoreApiSettings.ApiResource.Name, CoreApiSettings.ApiResource.DisplayName),
                new ApiResource(SalesApiSettings.ApiResource.Name, SalesApiSettings.ApiResource.DisplayName)
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // Core JavaScript Client
                new Client
                {
                    ClientId = CoreApiSettings.Client.ClientId,
                    ClientName = CoreApiSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { CoreApiSettings.Client.RedirectUris },
                    PostLogoutRedirectUris = { CoreApiSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { CoreApiSettings.Client.AllowedCorsOrigins },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        CoreApiSettings.ApiResource.Name
                    }
                },
                // Sales JavaScript Client
                new Client
                {
                    ClientId = SalesApiSettings.Client.ClientId,
                    ClientName = SalesApiSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { SalesApiSettings.Client.RedirectUris },
                    PostLogoutRedirectUris = { SalesApiSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { SalesApiSettings.Client.AllowedCorsOrigins },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        SalesApiSettings.ApiResource.Name,
                        CoreApiSettings.ApiResource.Name
                    }
                },
                // Sample
                //new Client
                //{
                //    ClientId = "mvc_code",
                //    ClientName = "MVC Client",
                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //    RequireConsent = true,
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    RedirectUris = { "http://localhost:5002/signin-oidc" },
                //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        CoreApiSettings.ApiResource.Name
                //    },
                //    AllowOfflineAccess = true,
                //    AllowAccessTokensViaBrowser = true
                //},
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
