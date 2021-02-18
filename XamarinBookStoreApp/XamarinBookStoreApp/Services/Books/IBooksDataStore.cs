using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.Books
{
    public interface IBooksDataStore<T>
    {
        Task<bool> AddBookAsync(T Book);
        Task<bool> UpdateBookAsync(T Book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<T> GetBookAsync(Guid id);
        Task<IEnumerable<T>> GetBooksAsync(bool forceRefresh = false);
    }
}
