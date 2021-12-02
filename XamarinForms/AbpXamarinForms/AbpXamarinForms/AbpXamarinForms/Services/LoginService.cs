using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace AbpXamarinForms.Services
{
    public class LoginService
    {
        private const string _authorityUrl = "https://e1c4-2a02-810d-98c0-576c-7857-2b49-dbd5-7275.eu.ngrok.io";
        private const string _redirectUrl = "xamarinformsclients:/authenticated";
        private const string _postLogoutRedirectUrl = "xamarinformsclients:/signout-callback-oidc";
        private const string _scopes = "email openid profile role phone address AbpApi";
        private const string _clientSecret = "1q2w3e*";
        private const string _clientId = "AbpApi_Xamarin";


        private OidcClient CreateOidcClient()
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