using XamarinBookStoreApi.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace XamarinBookStoreApi.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(XamarinBookStoreApiEntityFrameworkCoreDbMigrationsModule),
        typeof(XamarinBookStoreApiApplicationContractsModule)
        )]
    public class XamarinBookStoreApiDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
