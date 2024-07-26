using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Microservices.UserAPI;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("catalog_api"){Scopes = {"catalog_full_permission"}},
            new ApiResource("photostock_api"){Scopes = {"photo_stock_full_permission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResources.OpenId(),
            new IdentityResource(){ Name="roles", DisplayName="Roles", Description= "Kullanıcı Rolleri", UserClaims = new []{ "role" } }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalog_full_permission","Catalog API için erişim"),
            new ApiScope("photo_stock_full_permission","Photo Stock API için erişim"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
           new Client
           {
               ClientName= "",
               ClientId="Microservices",
               ClientSecrets ={new Secret("secret".Sha256())},
               AllowedGrantTypes = GrantTypes.ClientCredentials,
               AllowedScopes = { "catalog_full_permission", "photo_stock_full_permission", IdentityServerConstants.LocalApi.ScopeName }
           },
           new Client
           {
               ClientName= "",
               ClientId="MicroservicesForUser",
               ClientSecrets ={new Secret("secret".Sha256())},
               AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
               AllowedScopes = { IdentityServerConstants.StandardScopes.Email,
                   IdentityServerConstants.StandardScopes.Profile,
                   IdentityServerConstants.StandardScopes.OpenId,
                   IdentityServerConstants.StandardScopes.OfflineAccess,
                   IdentityServerConstants.LocalApi.ScopeName,
                   "roles"
               },
               AccessTokenLifetime = 1*60*60,
               RefreshTokenExpiration =  TokenExpiration.Absolute,
               AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) -  DateTime.Now).TotalSeconds,
               RefreshTokenUsage = TokenUsage.ReUse,
               AllowOfflineAccess = true
           }
        };
        

}

