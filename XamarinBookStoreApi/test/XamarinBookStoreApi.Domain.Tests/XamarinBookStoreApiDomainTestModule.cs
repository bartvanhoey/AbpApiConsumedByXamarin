using XamarinBookStoreApi.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace XamarinBookStoreApi
{
    [DependsOn(
        typeof(XamarinBookStoreApiEntityFrameworkCoreTestModule)
        )]
    public class XamarinBookStoreApiDomainTestModule : AbpModule
    {

    }
}