using System.Collections.Generic;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.Services.Books
{
    public class GetResult<T> where T : class 
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
