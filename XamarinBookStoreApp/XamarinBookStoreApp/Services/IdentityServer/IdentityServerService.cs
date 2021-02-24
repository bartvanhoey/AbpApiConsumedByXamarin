using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public class IdentityServerService : IIdentityServerService
    {
        public OidcClient OidcClient { get; private set; }

        private void SetOidcClient()
        {
            var browser = DependencyService.Get<IBrowser>();
            var options = new OidcClientOptions
            {
                Authority = "https://192.168.1.106:44368",
                ClientId = "XamarinBookStoreApi_Xamarin",
                Scope = "email openid profile role phone address XamarinBookStoreApi",
                ClientSecret = "1q2w3e*",
                RedirectUri = "xamarinformsclients://callback",
                Browser = browser,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };
            options.BackchannelHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true };
            options.Policy.Discovery.RequireHttps = true;
            OidcClient = new OidcClient(options);
        }

        public async Task<bool> LoginAsync()
        {
            SetOidcClient();
            var loginResult = await OidcClient.LoginAsync(new LoginRequest());


            if (!loginResult.IsError)
            {
                try
                {
                    await SecureStorage.SetAsync("identity_token", loginResult.IdentityToken);
                    foreach (var claim in loginResult.User.Claims)
                        await SecureStorage.SetAsync(claim.Type, claim.Value);

                    await SecureStorage.SetAsync("access_token", loginResult.AccessToken);
                    await SecureStorage.SetAsync("refresh_token", loginResult.RefreshToken);
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }
            }

            return !loginResult.IsError;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = await SecureStorage.GetAsync("access_token");
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(accessToken);
                var validTo = jwtToken.ValidTo;
                if (validTo <= DateTime.Now.AddMinutes(1))
                {
                    await LoginAsync();
                }
                else return accessToken;
            }
            else await LoginAsync();
            return await SecureStorage.GetAsync("access_token");
        }

        public async Task<bool> LogoutAsync()
        {
            SetOidcClient();
            SecureStorage.Remove("access_token");
            SecureStorage.Remove("refresh_token");
            LogoutResult logoutResult = null;
            var idTokenHint = await SecureStorage.GetAsync("identity_token");
            SecureStorage.Remove("identity_token");
            try
            {
                // TODO How to solve LogoutAsync not returning logoutResult
                logoutResult = await OidcClient.LogoutAsync(new LogoutRequest()
                {
                    BrowserDisplayMode = DisplayMode.Hidden,
                    IdTokenHint = idTokenHint,
                    BrowserTimeout = 1000
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return !logoutResult.IsError;
        }
    }
}
