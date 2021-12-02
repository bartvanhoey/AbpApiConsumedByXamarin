using System;
using AbpApi.Domain.Shared.Books;

namespace AbpApi.Application.Contracts.Books
{
    public class UpdateBookDto
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
    }
}
