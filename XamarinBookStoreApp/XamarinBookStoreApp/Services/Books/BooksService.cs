using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Services.IdentityServer;


namespace XamarinBookStoreApp.Services.Books
{
    public class BooksService : IBooksService
    {
        public IBooksDataStore<BookDto> BooksDataStore => DependencyService.Get<IBooksDataStore<BookDto>>();
        public IIdentityServerService IdentityService => DependencyService.Get<IIdentityServerService>();
        Lazy<HttpClient> _apiClient;

        private async Task<string> GetAccessTokenAsync() => await IdentityService.GetAccessTokenAsync();

        public async Task<IEnumerable<BookDto>> GetListAsync()
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

                if (booksResult.TotalCount > 0)
                {
                    await BooksDataStore.DeleteAllBookAsync();
                    foreach (var bookDto in booksResult.Items)
                    {
                        await BooksDataStore.AddBookAsync(bookDto);
                    }
                }
            }
            return await BooksDataStore.GetBooksAsync(true);
        }

        public Task<BookDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> CreateAsync(CreateBookDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateBookDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
