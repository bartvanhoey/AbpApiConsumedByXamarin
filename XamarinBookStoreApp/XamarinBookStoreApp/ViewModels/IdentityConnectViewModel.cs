using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.IdentityServer;

namespace XamarinBookStoreApp.ViewModels
{
    public partial class IdentityConnectViewModel : BaseViewModel
    {
        IIdentityServerService IdentityServerService => DependencyService.Get<IIdentityServerService>();
        Lazy<HttpClient> _apiClient;
        public Command ConnectToIdentityServerCommand { get; }

        public IdentityConnectViewModel()
        {
            Title = "IdentityServer";
            ConnectToIdentityServerCommand = new Command(async () => await ConnectToIdentityServerAsync());
        }

        private async Task ConnectToIdentityServerAsync()
        {
            // connect to IdentityServer
            var loginResult = await IdentityServerService.LoginAysnc();
            if (!loginResult) return;


            // get accesstoken
            var accessToken = await SecureStorage.GetAsync("access_token");


            // call books API
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            _apiClient = new Lazy<HttpClient>(() => new HttpClient(httpClientHandler));

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken ?? "");
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
