using XamarinBookStoreApi.Localization;
using Volo.Abp.AspNetCore.Components;

namespace XamarinBookStoreApi.Blazor
{
    public abstract class XamarinBookStoreApiComponentBase : AbpComponentBase
    {
        protected XamarinBookStoreApiComponentBase()
        {
            LocalizationResource = typeof(XamarinBookStoreApiResource);
        }
    }
}
