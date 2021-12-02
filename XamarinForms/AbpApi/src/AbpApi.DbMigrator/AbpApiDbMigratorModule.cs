using AbpApi.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AbpApi.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpApiEntityFrameworkCoreModule),
        typeof(AbpApiApplicationContractsModule)
        )]
    public class AbpApiDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
