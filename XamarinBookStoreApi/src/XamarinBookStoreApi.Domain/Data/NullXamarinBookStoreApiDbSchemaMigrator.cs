using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XamarinBookStoreApi.Data
{
    /* This is used if database provider does't define
     * IXamarinBookStoreApiDbSchemaMigrator implementation.
     */
    public class NullXamarinBookStoreApiDbSchemaMigrator : IXamarinBookStoreApiDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}