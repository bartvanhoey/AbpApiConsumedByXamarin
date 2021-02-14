using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XamarinBookStoreApi.Data;
using Volo.Abp.DependencyInjection;

namespace XamarinBookStoreApi.EntityFrameworkCore
{
    public class EntityFrameworkCoreXamarinBookStoreApiDbSchemaMigrator
        : IXamarinBookStoreApiDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreXamarinBookStoreApiDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the XamarinBookStoreApiMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<XamarinBookStoreApiMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}