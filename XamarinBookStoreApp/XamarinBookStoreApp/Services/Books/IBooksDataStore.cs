using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.Books
{
    public interface IBooksDataStore<T>
    {
        Task<bool> AddBookAsync(T Book);
        Task<bool> UpdateBookAsync(T Book);
        Task<bool> DeleteBookAsync(string id);
        Task<T> GetBookAsync(string id);
        Task<IEnumerable<T>> GetBooksAsync(bool forceRefresh = false);
    }
}
