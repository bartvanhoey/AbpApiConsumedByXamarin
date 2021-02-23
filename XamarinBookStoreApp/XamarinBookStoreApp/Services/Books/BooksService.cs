using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Services.Http;
using XamarinBookStoreApp.Services.IdentityServer;


namespace XamarinBookStoreApp.Services.Books
{
    public class BooksService : IBooksService
    {
        public IBooksDataStore<BookDto> BooksDataStore => DependencyService.Get<IBooksDataStore<BookDto>>();
        public IHttpClientService<BookDto, CreateBookDto> HttpClient => DependencyService.Get<IHttpClientService<BookDto, CreateBookDto>>();

        public async Task<IEnumerable<BookDto>> GetListAsync()
        {
            var books = await HttpClient.GetAsync(GlobalSettings.Instance.GetBooksUri);
            if (books.TotalCount > 0)
            {
                await BooksDataStore.DeleteAllBookAsync();
                foreach (var bookDto in books.Items)
                {
                    await BooksDataStore.AddBookAsync(bookDto);
                }
            }
            return await BooksDataStore.GetBooksAsync(true);
        }

        public Task<BookDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BookDto> CreateAsync(CreateBookDto input)
        {
            return await HttpClient.CreateAsync(GlobalSettings.Instance.PostBookUri, input);
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
