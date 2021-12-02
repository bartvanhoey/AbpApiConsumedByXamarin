using AbpApi.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpApi
{
    [DependsOn(
        typeof(AbpApiEntityFrameworkCoreTestModule)
        )]
    public class AbpApiDomainTestModule : AbpModule
    {

    }
}