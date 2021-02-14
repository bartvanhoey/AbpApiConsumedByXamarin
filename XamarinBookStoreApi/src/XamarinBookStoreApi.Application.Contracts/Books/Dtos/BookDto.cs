using System;
using Volo.Abp.Application.Dtos;
using XamarinBookStoreApi.Domain.Shared.Books;

namespace XamarinBookStoreApi.Application.Contracts.Books.Dtos
{
   public class BookDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}