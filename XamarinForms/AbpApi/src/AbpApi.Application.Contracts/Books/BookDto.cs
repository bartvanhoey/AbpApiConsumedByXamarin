using System;
using AbpApi.Domain.Shared.Books;
using Volo.Abp.Application.Dtos;

namespace AbpApi.Application.Contracts.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
    }
}
