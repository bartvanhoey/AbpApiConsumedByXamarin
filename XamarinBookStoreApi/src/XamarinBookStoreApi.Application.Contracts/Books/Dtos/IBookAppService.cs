using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace XamarinBookStoreApi.Application.Contracts.Books.Dtos
{
  public interface IBookAppService : ICrudAppService<BookDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateBookDto>
  {

  }
}

