using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace XamarinBookStoreApi.EntityFrameworkCore
{
    [DependsOn(
        typeof(XamarinBookStoreApiEntityFrameworkCoreModule)
        )]
    public class XamarinBookStoreApiEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<XamarinBookStoreApiMigrationsDbContext>();
        }
    }
}
