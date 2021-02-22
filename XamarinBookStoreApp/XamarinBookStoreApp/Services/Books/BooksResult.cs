using System.Collections.Generic;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.Services.Books
{
    public class BooksResult
    {
        public int TotalCount { get; set; }
        public List<BookDto> Items { get; set; }
    }
}
