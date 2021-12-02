using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpApi.Data
{
    /* This is used if database provider does't define
     * IAbpApiDbSchemaMigrator implementation.
     */
    public class NullAbpApiDbSchemaMigrator : IAbpApiDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}