using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.IdentityServer;

namespace XamarinBookStoreApp.Services.Books
{
    public class BooksService : IBooksService
    {
        public IBooksDataStore<Book> BooksDataStore => DependencyService.Get<IBooksDataStore<Book>>();
        public IIdentityServerService IdentityService => DependencyService.Get<IIdentityServerService>();
        Lazy<HttpClient> _apiClient;

        private async Task<string> GetAccessTokenAsync() => await IdentityService.GetAccessTokenAsync();

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var accessToken = await GetAccessTokenAsync();
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
                    var book = new Book
                    {
                        Id = bookDto.Id,
                        Name = bookDto.Name,
                        Price = bookDto.Price,
                        PublishDate = bookDto.PublishDate,
                        Type = bookDto.Type
                    };
                    await BooksDataStore.AddBookAsync(book);
                }
            }
            return await BooksDataStore.GetBooksAsync(true);
        }
    }
}
