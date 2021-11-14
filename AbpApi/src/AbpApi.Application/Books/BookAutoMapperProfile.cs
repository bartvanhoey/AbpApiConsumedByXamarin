using System;
using AbpApi.Application.Contracts.Books;
using AbpApi.Domain.Books;
using AutoMapper;

namespace AbpApi.Application.Books
{
    public class BookAutoMapperProfile : Profile
    {
        public BookAutoMapperProfile()
        {
            CreateMap<BookDto,Book>();
            CreateMap<Book,BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
