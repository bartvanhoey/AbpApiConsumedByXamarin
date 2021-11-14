using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AbpApi.Data;
using Volo.Abp.DependencyInjection;

namespace AbpApi.EntityFrameworkCore
{
    public class EntityFrameworkCoreAbpApiDbSchemaMigrator
        : IAbpApiDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAbpApiDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AbpApiDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AbpApiDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
