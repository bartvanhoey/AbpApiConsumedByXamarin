using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<bool> LoginAysnc()
        {
            SetOidcClient();
            var loginResult = await OidcClient.LoginAsync(new LoginRequest());

            if (!loginResult.IsError)
            {
                try
                {
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
    }
}
