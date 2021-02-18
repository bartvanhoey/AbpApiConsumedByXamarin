using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.IdentityServer;

namespace XamarinBookStoreApp.ViewModels
{
    public partial class IdentityConnectViewModel : BaseViewModel
    {
        IIdentityServerService IdentityServerService => DependencyService.Get<IIdentityServerService>();
        OidcClient _client;
        LoginResult _result;
        Lazy<HttpClient> _apiClient;
        //public IBooksDataStore<Book> BooksDataStore => DependencyService.Get<IBooksDataStore<Book>>();
        public Command ConnectToIdentityServerCommand { get; }


        public IdentityConnectViewModel()
        {
            Title = "IdentityServer";

            

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
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                 (message, cert, chain, errors) => { return true; }
            };

            _apiClient = new Lazy<HttpClient>(() => new HttpClient(httpClientHandler));

            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError) return;

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _result?.AccessToken ?? "");
            var response = await _apiClient.Value.GetAsync("https://192.168.1.106:44323/api/app/book");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var booksResult = JsonConvert.DeserializeObject<BooksResult>(content);

                foreach (var bookDto in booksResult.Items)
                {
                    await BooksDataStore.AddBookAsync(new Book
                    {
                        Id = bookDto.Id,
                        Name = bookDto.Name,
                        Price = bookDto.Price,
                        PublishDate = bookDto.PublishDate,
                        Type = bookDto.Type
                    });
                }
                await Shell.Current.GoToAsync("//BooksPage");
            }
        }
    }
}
