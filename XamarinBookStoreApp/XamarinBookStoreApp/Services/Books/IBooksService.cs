using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books.Dtos;


namespace XamarinBookStoreApp.Services.Books
{
    public interface IBooksService
    {
        //Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input);
        Task<IEnumerable<BookDto>> GetListAsync();
        Task<BookDto> GetAsync(Guid id);
        Task<BookDto> CreateAsync(CreateBookDto input);
        Task UpdateAsync(Guid id, UpdateBookDto input);
        Task DeleteAsync(Guid id);
    }
}
