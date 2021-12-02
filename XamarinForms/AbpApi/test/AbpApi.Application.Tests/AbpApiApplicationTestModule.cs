using Volo.Abp.Modularity;

namespace AbpApi
{
    [DependsOn(
        typeof(AbpApiApplicationModule),
        typeof(AbpApiDomainTestModule)
        )]
    public class AbpApiApplicationTestModule : AbpModule
    {

    }
}