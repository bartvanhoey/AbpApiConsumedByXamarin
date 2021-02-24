using System.Collections.Generic;

namespace XamarinBookStoreApp.Services.Books.Dtos
{
    public class ListResult<T> where T : class 
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
