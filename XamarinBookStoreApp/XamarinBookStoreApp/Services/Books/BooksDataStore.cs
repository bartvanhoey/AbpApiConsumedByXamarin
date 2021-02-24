using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.Services
{
    public class BooksDataStore : IBooksDataStore<BookDto>
    {
        readonly List<BookDto> books;

        public BooksDataStore()
        {
            books = new List<BookDto>();
        }

        public async Task<bool> CreateBookAsync(BookDto book)
        {
            books.Add(book);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateBookAsync(BookDto book)
        {
            var oldBook = books.Where((BookDto arg) => arg.Id == book.Id).FirstOrDefault();
            books.Remove(oldBook);
            books.Add(book);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var oldBook = books.Where((BookDto arg) => arg.Id == id).FirstOrDefault();
            books.Remove(oldBook);
            return await Task.FromResult(true);
        }

        public async Task<BookDto> GetBookAsync(Guid id)
        {
            return await Task.FromResult(books.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(books);
        }

        public async Task<bool> DeleteAllBooksAsync()
        {
            books.Clear();
            return await Task.FromResult(true);
        }
    }
}