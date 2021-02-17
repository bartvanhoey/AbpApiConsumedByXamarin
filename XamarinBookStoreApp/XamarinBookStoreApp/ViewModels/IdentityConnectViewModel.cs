using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinBookStoreApp.ViewModels
{
    public class IdentityConnectViewModel : BaseViewModel
    {
        OidcClient _client;
        LoginResult _result;
        Lazy<HttpClient> _apiClient;

        public Command ConnectToIdentityServerCommand { get; }


        public IdentityConnectViewModel()
        {
            Title = "IdentityServer";

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                 (message, cert, chain, errors) => { return true; }
            };

            _apiClient = new Lazy<HttpClient>(() => new HttpClient(httpClientHandler));

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
            _client = new OidcClient(options);

            ConnectToIdentityServerCommand = new Command(async () => await ConnectToIdentityServerAsync());
        }

        private async Task ConnectToIdentityServerAsync()
        {
            string outputText;
            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError)
            {
                outputText = _result.Error;
                return;
            }

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            outputText = sb.ToString();


            //Application.Current.Properties["accessToken"] = _result?.AccessToken;

            //await SecureStorage.SetAsync("accessToken", _result.AccessToken);

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _result?.AccessToken ?? "");
            var response = await _apiClient.Value.GetAsync("https://192.168.1.106:44323/api/app/book");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BooksResult>(content);
            }





        }

        public class BooksResult
        {
            public int TotalCount { get; set; }
            public List<BookDto> Items { get; set; }
        }

        public class BookDto
        {

            public Guid Id { get; set; }
            public string Name { get; set; }

            public BookType Type { get; set; }

            public DateTime PublishDate { get; set; }

            public float Price { get; set; }

            public DateTime? LastModificationTime { get; set; }
            public Guid? LastModifierId { get; set; }
        }

        public enum BookType
        {
            Undefined,
            Adventure,
            Biography,
            Dystopia,
            Fantastic,
            Horror,
            Science,
            ScienceFiction,
            Poetry
        }
    }
}
