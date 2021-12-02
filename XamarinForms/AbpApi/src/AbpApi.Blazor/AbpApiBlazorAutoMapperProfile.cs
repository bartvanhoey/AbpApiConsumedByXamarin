using AbpApi.Application.Contracts.Books;
using AutoMapper;

namespace AbpApi.Blazor
{
    public class AbpApiBlazorAutoMapperProfile : Profile
    {
        public AbpApiBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.

            CreateMap<BookDto, UpdateBookDto>();
        }
    }
}
