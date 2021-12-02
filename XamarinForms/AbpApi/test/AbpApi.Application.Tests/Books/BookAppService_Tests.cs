using System;
using System.Linq;
using System.Threading.Tasks;
using AbpApi.Application.Contracts.Books;
using AbpApi.Domain.Shared.Books;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace AbpApi.Application.Tests.Books
{
    public class BookAppService_Tests : AbpApiApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(i => i.Name == "1984");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            const string newBook = "New Test book 42";
            var result = await _bookAppService.CreateAsync(
                 new CreateBookDto
                 {
                     Name = newBook,
                     PublishDate = DateTime.Now,
                     Price = 10,
                     Type = BookType.ScienceFiction
                 }
             );

            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe(newBook);
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_A_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
            var result = await _bookAppService.CreateAsync(
                new CreateBookDto
                {
                    Name = "",
                    PublishDate = DateTime.Now,
                    Price = 10,
                    Type = BookType.ScienceFiction
                }
            );
            });
            
            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));

        }
    }
}
