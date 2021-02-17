using System.Collections.Generic;
using XamarinBookStoreApp.ViewModels.Services.Books;

namespace XamarinBookStoreApp.Services.Books
{
    public class BooksResult
    {
        public int TotalCount { get; set; }
        public List<BookDto> Items { get; set; }
    }
}
