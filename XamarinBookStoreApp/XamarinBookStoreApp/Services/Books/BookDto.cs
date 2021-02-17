using System;

namespace XamarinBookStoreApp.ViewModels.Services.Books
{

    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }

}
