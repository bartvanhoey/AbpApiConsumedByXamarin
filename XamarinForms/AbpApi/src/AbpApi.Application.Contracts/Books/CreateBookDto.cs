using System;
using System.ComponentModel.DataAnnotations;
using AbpApi.Domain.Shared.Books;

namespace AbpApi.Application.Contracts.Books
{
    public class CreateBookDto
    {
        [Required] 
        [StringLength(128)]
        public string Name { get; set; }
        
        [Required] 
        public BookType Type { get; set; }
        
        [Required] 
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        
        [Required] 
        public float Price { get; set; }
    }
}
