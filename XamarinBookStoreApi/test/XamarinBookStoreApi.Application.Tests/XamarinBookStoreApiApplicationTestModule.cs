using Volo.Abp.Modularity;

namespace XamarinBookStoreApi
{
    [DependsOn(
        typeof(XamarinBookStoreApiApplicationModule),
        typeof(XamarinBookStoreApiDomainTestModule)
        )]
    public class XamarinBookStoreApiApplicationTestModule : AbpModule
    {

    }
}