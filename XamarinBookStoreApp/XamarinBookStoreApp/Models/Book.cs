using System;
using System.Collections.Generic;
using System.Text;
using XamarinBookStoreApp.ViewModels.Services.Books;

namespace XamarinBookStoreApp.Models
{
    public class Book
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
    }
}
