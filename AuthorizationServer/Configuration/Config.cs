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
                new ApiResource(AuthorizationServerSettings.ApiResource.Name, AuthorizationServerSettings.ApiResource.DisplayName),
                new ApiResource(CoreApiSettings.CoreApiResource.Name, CoreApiSettings.CoreApiResource.DisplayName),
                new ApiResource("socialnetwork", "社交网络")
                {
                    UserClaims = new [] { "email" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = AuthorizationServerSettings.Client.ClientId,
                    ClientSecrets = new [] { new Secret(AuthorizationServerSettings.Client.ClientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        AuthorizationServerSettings.ApiResource.Name
                    }
                },
                // Admin JavaScript Client
                new Client
                {
                    ClientId = AdminClientSettings.Client.ClientId,
                    ClientName = AdminClientSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { AdminClientSettings.Client.RedirectUris },
                    PostLogoutRedirectUris = { AdminClientSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { AdminClientSettings.Client.AllowedCorsOrigins },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        AdminClientSettings.CoreApiResource.Name
                    }
                },
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                },
                new Client
                {
                    ClientId = "mvc_code",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        CoreApiSettings.CoreApiResource.Name,
                        "socialnetwork"
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true
                },
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
                        CoreApiSettings.CoreApiResource.Name
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
