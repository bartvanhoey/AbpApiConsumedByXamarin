using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XamarinBookStoreApi.Domain.Books;

namespace XamarinBookStoreApi.EntityFrameworkCore
{
    public static class XamarinBookStoreApiDbContextModelCreatingExtensions
    {
        public static void ConfigureXamarinBookStoreApi(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(XamarinBookStoreApiConsts.DbTablePrefix + "YourEntities", XamarinBookStoreApiConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Book>(b =>
            {
                b.ToTable(XamarinBookStoreApiConsts.DbTablePrefix + "Books", XamarinBookStoreApiConsts.DbSchema);
                b.ConfigureByConvention();
            
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
        }
    }
}