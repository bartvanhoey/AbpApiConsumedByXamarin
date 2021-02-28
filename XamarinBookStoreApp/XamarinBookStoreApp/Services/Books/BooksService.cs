using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Services.Http;


namespace XamarinBookStoreApp.Services.Books
{
    public class BooksService : IBooksService
    {
        public IBooksDataStore<BookDto> BooksDataStore => DependencyService.Get<IBooksDataStore<BookDto>>();
        public IHttpClientService<BookDto, CreateBookDto> HttpClient => DependencyService.Get<IHttpClientService<BookDto, CreateBookDto>>();

        public async Task<IEnumerable<BookDto>> GetListAsync()
        {
            var books = await HttpClient.GetAsync(Global.Settings.Api.GetBooksUri);
            if (books.TotalCount > 0)
            {
                await BooksDataStore.DeleteAllBooksAsync();
                foreach (var bookDto in books.Items)
                {
                    await BooksDataStore.CreateBookAsync(bookDto);
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
            var bookDto = await HttpClient.CreateAsync(Global.Settings.Api.PostBookUri, input);
            await BooksDataStore.CreateBookAsync(bookDto);
            return bookDto;
        }

        public Task UpdateAsync(Guid id, UpdateBookDto input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await HttpClient.DeleteAsync(Global.Settings.Api.DeleteBookUri, id.ToString());
        }
    }
}
