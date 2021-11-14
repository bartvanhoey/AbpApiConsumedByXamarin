using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AbpApi
{
    [Dependency(ReplaceServices = true)]
    public class AbpApiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AbpApi";
    }
}
