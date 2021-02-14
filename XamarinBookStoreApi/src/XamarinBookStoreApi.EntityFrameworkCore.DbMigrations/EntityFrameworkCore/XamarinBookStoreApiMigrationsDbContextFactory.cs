using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace XamarinBookStoreApi.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class XamarinBookStoreApiMigrationsDbContextFactory : IDesignTimeDbContextFactory<XamarinBookStoreApiMigrationsDbContext>
    {
        public XamarinBookStoreApiMigrationsDbContext CreateDbContext(string[] args)
        {
            XamarinBookStoreApiEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<XamarinBookStoreApiMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new XamarinBookStoreApiMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../XamarinBookStoreApi.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
