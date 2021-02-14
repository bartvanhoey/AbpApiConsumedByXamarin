using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XamarinBookStoreApi.Application.Contracts.Books.Dtos;
using XamarinBookStoreApi.Domain.Books;
using XamarinBookStoreApi.Permissions;

namespace XamarinBookStoreApi.Application.Books
{
  public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, IBookAppService
  {
      public BookAppService(IRepository<Book, Guid> repository): base(repository)
      {
          GetPolicyName = XamarinBookStoreApiPermissions.Books.Default;
          GetListPolicyName = XamarinBookStoreApiPermissions.Books.Default;
          CreatePolicyName = XamarinBookStoreApiPermissions.Books.Create;
          UpdatePolicyName = XamarinBookStoreApiPermissions.Books.Edit;
          DeletePolicyName = XamarinBookStoreApiPermissions.Books.Delete;
      }
  }
}