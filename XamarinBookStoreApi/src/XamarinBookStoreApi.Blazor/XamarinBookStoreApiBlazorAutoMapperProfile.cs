using AutoMapper;
using XamarinBookStoreApi.Application.Contracts.Books.Dtos;

namespace XamarinBookStoreApi.Blazor
{
    public class XamarinBookStoreApiBlazorAutoMapperProfile : Profile
    {
        public XamarinBookStoreApiBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.

              CreateMap<BookDto, CreateUpdateBookDto>();
        }
    }
}
