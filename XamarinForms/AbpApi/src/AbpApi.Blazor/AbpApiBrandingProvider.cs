using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AbpApi.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class AbpApiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AbpApi";
    }
}
