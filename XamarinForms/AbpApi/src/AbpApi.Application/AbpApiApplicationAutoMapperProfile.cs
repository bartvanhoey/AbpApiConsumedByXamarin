using AbpApi.Application.Contracts.Books;
using AbpApi.Domain.Books;
using AutoMapper;

namespace AbpApi
{
    public class AbpApiApplicationAutoMapperProfile : Profile
    {
        public AbpApiApplicationAutoMapperProfile()
        {


            CreateMap<Book, BookDto>();

            CreateMap<UpdateBookDto, Book>();

            CreateMap<CreateBookDto, Book>();

            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}
