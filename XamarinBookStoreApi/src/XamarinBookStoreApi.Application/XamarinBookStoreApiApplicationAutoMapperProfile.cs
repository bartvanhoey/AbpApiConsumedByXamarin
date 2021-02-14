using AutoMapper;
using XamarinBookStoreApi.Application.Contracts.Books.Dtos;
using XamarinBookStoreApi.Domain.Books;

namespace XamarinBookStoreApi
{
    public class XamarinBookStoreApiApplicationAutoMapperProfile : Profile
    {
        public XamarinBookStoreApiApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

             CreateMap<Book, BookDto>();
             CreateMap<CreateUpdateBookDto, Book>();
        }
    }
}
