using AbpMauiApp.Services;
using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace AbpMaui.Services
{
    public class LoginService
    {
        private const string _authorityUrl = "https://7bb4-2a02-810d-98c0-576c-a0b1-8ff0-f58c-8179.eu.ngrok.io";
        private const string _redirectUrl = "mauiclients:/authenticated";
        private const string _postLogoutRedirectUrl = "mauiclients:/signout-callback-oidc";
        private const string _scopes = "email openid profile role phone address AbpApi";
        private const string _clientSecret = "1q2w3e*";
        private const string _clientId = "AbpApi_Maui";


        private static OidcClient CreateOidcClient()
        {
            var options = new OidcClientOptions
            {
                Authority = _authorityUrl,
                ClientId = _clientId,
                Scope = _scopes,
                RedirectUri = _redirectUrl,
                ClientSecret = _clientSecret,
                PostLogoutRedirectUri = _postLogoutRedirectUrl,
                Browser = new WebAuthenticatorBrowser()
            };
            return new OidcClient(options);
        }

        public async Task<string> AuthenticateAsync()
        {
            var oidcClient = CreateOidcClient();
            var loginResult = await oidcClient.LoginAsync(new LoginRequest());
            return loginResult.AccessToken;
        }
    }
}