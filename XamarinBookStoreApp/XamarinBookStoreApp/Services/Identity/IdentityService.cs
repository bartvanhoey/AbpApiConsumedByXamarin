using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public class IdentityService : IIdentityService
    {
        private const string AccessToken = "access_token";
        private const string IdentityToken = "identity_token";

        public OidcClient OidcClient { get; private set; }

        private void SetOidcClient()
        {
            var browser = DependencyService.Get<IBrowser>();
            var options = new OidcClientOptions
            {
                Authority = Global.Settings.IdentityServer.Authority,
                ClientId = Global.Settings.IdentityServer.ClientId,
                Scope = Global.Settings.IdentityServer.Scope,
                ClientSecret = Global.Settings.IdentityServer.ClientSecret,
                RedirectUri = Global.Settings.IdentityServer.RedirectUri,
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
            if (loginResult.IsError) return false;
            return await WriteTokensAndClaimsToSecureStorageAsync(loginResult);
        }

        private async Task<bool> WriteTokensAndClaimsToSecureStorageAsync(LoginResult loginResult)
        {
            try
            {
                var customClaims = ExtractCustomClaims(loginResult.AccessToken);
                var globalSettingsPossiblePermissions = Global.Settings.PossiblePermissions.Get;
                foreach (var permission in globalSettingsPossiblePermissions)
                {
                    var containsKey = customClaims.ContainsKey($"client_{permission}");
                    var canClaim = containsKey == true && customClaims[$"client_{permission}"].Value<bool>();
                    await SecureStorage.SetAsync(permission, canClaim.ToString());
                }
                await SecureStorage.SetAsync(IdentityToken, loginResult.IdentityToken);
                foreach (var claim in loginResult.User.Claims)
                    await SecureStorage.SetAsync(claim.Type, claim.Value);

                await SecureStorage.SetAsync(AccessToken, loginResult.AccessToken);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                // Possible that device doesn't support secure storage on device.
                throw new ArgumentException("device doesn't support secure storage on device");
            }
            return true;
        }

        public JObject ExtractCustomClaims(string accessToken)
        {
            var base64payload = accessToken.Split('.')[1];
            base64payload = base64payload.PadRight(base64payload.Length + (base64payload.Length * 3) % 4, '=');  // add padding
            var bytes = Convert.FromBase64String(base64payload);
            var jsonPayload = Encoding.UTF8.GetString(bytes);
            var claimsFromAccessToken = JObject.Parse(jsonPayload);
            return claimsFromAccessToken;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = await SecureStorage.GetAsync(AccessToken);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(accessToken);
                var validTo = jwtToken.ValidTo;
                if (validTo <= DateTime.Now.AddMinutes(5))
                {
                    await LoginAsync();
                }
                else return accessToken;
            }
            else await LoginAsync();
            return await SecureStorage.GetAsync(AccessToken);
        }

        public async Task<bool> LogoutAsync()
        {
            SetOidcClient();
            SecureStorage.Remove(AccessToken);
            var idTokenHint = await SecureStorage.GetAsync(IdentityToken);
            SecureStorage.Remove(IdentityToken);
            LogoutResult logoutResult;
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
                throw new Exception(ex.Message);
            }
            return !logoutResult.IsError;
        }
    }
}
