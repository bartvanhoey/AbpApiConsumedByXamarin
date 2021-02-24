using System;
using XamarinBookStoreApp.DomainShared.Books;

namespace XamarinBookStoreApp.Services.Books.Dtos
{

    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public string PublishDatePrice
        {
            get
            {
                return string.Format("Publish date: {0:ddd. d-MM-yyyy} - price: {1}", PublishDate, Price);
            }
        }
    }

}
