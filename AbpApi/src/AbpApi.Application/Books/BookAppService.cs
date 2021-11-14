using System;
using AbpApi.Application.Contracts.Books;
using AbpApi.Domain.Books;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpApi.Application.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateBookDto, UpdateBookDto>, IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository): base(repository)
        {
            // GetPolicyName = AbpApiPermissions.Books.Default;
            // GetListPolicyName = AbpApiPermissions.Books.Default;
            // CreatePolicyName = AbpApiPermissions.Books.Create;
            // UpdatePolicyName = AbpApiPermissions.Books.Update;
            // DeletePolicyName = AbpApiPermissions.Books.Delete;
        }
    }
}
