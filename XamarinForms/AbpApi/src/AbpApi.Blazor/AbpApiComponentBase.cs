using AbpApi.Localization;
using Volo.Abp.AspNetCore.Components;

namespace AbpApi.Blazor
{
    public abstract class AbpApiComponentBase : AbpComponentBase
    {
        protected AbpApiComponentBase()
        {
            LocalizationResource = typeof(AbpApiResource);
        }
    }
}
