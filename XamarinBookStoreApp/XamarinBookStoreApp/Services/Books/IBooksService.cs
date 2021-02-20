using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBookStoreApp.Models;

namespace XamarinBookStoreApp.Services.Books
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }
}
