using System;
using XamarinBookStoreApp.Models.Books;

namespace XamarinBookStoreApp.Services.Books.Dtos
{
    public class CreateBookDto
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
    }
}