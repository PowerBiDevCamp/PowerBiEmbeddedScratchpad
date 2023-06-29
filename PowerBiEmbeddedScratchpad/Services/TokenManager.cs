using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using PowerBiEmbeddedScratchpad.Models;

namespace PowerBiEmbeddedScratchpad.Services {

  class TokenManager {

    public const string urlPowerBiServiceApiRoot = "https://api.powerbi.com/";
    private const string tenantCommonAuthority = "https://login.microsoftonline.com/organizations";

    // Azure AD Application Id for user authentication
    private static string applicationId = AppSettings.ApplicationId;
    private static string redirectUri = AppSettings.RedirectUri;

    // Azure AD Application Id for service principal authentication
    private static string confidentialApplicationId = AppSettings.ConfidentialApplicationId;
    private static string confidentialApplicationSecret = AppSettings.ConfidentialApplicationSecret;
    private static string tenantSpecificAuthority = AppSettings.TenantSpecificAuthority;

    public static string GetAccessTokenInteractive(string[] scopes) {

      // create new public client application
      var appPublic = PublicClientApplicationBuilder.Create(applicationId)
                    .WithAuthority(tenantCommonAuthority)
                    .WithRedirectUri(redirectUri)
                    .Build();

      AuthenticationResult authResult = appPublic.AcquireTokenInteractive(scopes).ExecuteAsync().Result;

      // return access token to caller
      return authResult.AccessToken;
    }

    public static string GetAccessToken(string[] scopes) {

      // create new public client application
      var appPublic = PublicClientApplicationBuilder.Create(applicationId)
                      .WithAuthority(tenantCommonAuthority)
                      .WithRedirectUri(redirectUri)
                      .Build();

      // connect application to token cache
      TokenCacheHelper.EnableSerialization(appPublic.UserTokenCache);

      AuthenticationResult authResult;
      try {
        // try to acquire token from token cache
        var user = appPublic.GetAccountsAsync().Result.FirstOrDefault();
        authResult = appPublic.AcquireTokenSilent(scopes, user).ExecuteAsync().Result;
      }
      catch {
        authResult = appPublic.AcquireTokenInteractive(scopes).ExecuteAsync().Result;
      }

      // return access token to caller
      return authResult.AccessToken;
    }

    public static string GetAccessToken() {
      return GetAccessToken(PowerBiPermissionScopes.TenantProvisioning);
    }

    public static string GetAccessTokenForKustoDatabase() {
      var appPublic = PublicClientApplicationBuilder.Create(applicationId)
                    .WithAuthority(tenantCommonAuthority)
                    .WithRedirectUri(redirectUri)
                    .Build();

      // create scopes array to get access token for SharePoint site
      string[] KustoDatabaseScopes = new string[] {
        "https://kusto.eastus2.kusto.windows.net/user_impersonation"
      };

      // connect application to token cache
      TokenCacheHelper.EnableSerialization(appPublic.UserTokenCache);

      AuthenticationResult authResult = appPublic.AcquireTokenInteractive(KustoDatabaseScopes).ExecuteAsync().Result;

      // return access token to caller
      return authResult.AccessToken;

    }

    public static PowerBIClient GetPowerBiClientForUser() {
      var tokenCredentials = new TokenCredentials(GetAccessToken(), "Bearer");
      return new PowerBIClient(new Uri(urlPowerBiServiceApiRoot), tokenCredentials);
    }

    public static PowerBIClient GetPowerBiClientForUser(string[] scopes) {
      var tokenCredentials = new TokenCredentials(GetAccessToken(scopes), "Bearer");
      return new PowerBIClient(new Uri(urlPowerBiServiceApiRoot), tokenCredentials);
    }

    static class TokenCacheHelper {

      private static readonly string CacheFilePath = Assembly.GetExecutingAssembly().Location + ".tokencache.json";
      private static readonly object FileLock = new object();

      public static void EnableSerialization(ITokenCache tokenCache) {
        tokenCache.SetBeforeAccess(BeforeAccessNotification);
        tokenCache.SetAfterAccess(AfterAccessNotification);
      }

      private static void BeforeAccessNotification(TokenCacheNotificationArgs args) {
        lock (FileLock) {
          // repopulate token cache from persisted store
          args.TokenCache.DeserializeMsalV3(File.Exists(CacheFilePath) ? File.ReadAllBytes(CacheFilePath) : null);
        }
      }

      private static void AfterAccessNotification(TokenCacheNotificationArgs args) {
        // if the access operation resulted in a cache update
        if (args.HasStateChanged) {
          lock (FileLock) {
            // write token cache changes to persistent store
            File.WriteAllBytes(CacheFilePath, args.TokenCache.SerializeMsalV3());
          }
        }
      }
    }

    public static string GetAccessTokenForServicePrincipal(string[] scopes = null) {

      if (scopes == null || scopes.Length == 0) {
        scopes = new string[] { "https://analysis.windows.net/powerbi/api/.default" };
      }

      var appConfidential = ConfidentialClientApplicationBuilder.Create(confidentialApplicationId)
                              .WithClientSecret(confidentialApplicationSecret)
                              .WithAuthority(tenantSpecificAuthority)
                              .Build();


      var authResult = appConfidential.AcquireTokenForClient(scopes).ExecuteAsync().Result;
      return authResult.AccessToken;
    }

    public static PowerBIClient GetPowerBiClientForServicePrincipal() {
      var tokenCredentials = new TokenCredentials(GetAccessTokenForServicePrincipal(), "Bearer");
      return new PowerBIClient(new Uri(urlPowerBiServiceApiRoot), tokenCredentials);
    }

  }

}
