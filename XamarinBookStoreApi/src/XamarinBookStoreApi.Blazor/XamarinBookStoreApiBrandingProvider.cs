using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace XamarinBookStoreApi.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class XamarinBookStoreApiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "XamarinBookStoreApi";
    }
}
