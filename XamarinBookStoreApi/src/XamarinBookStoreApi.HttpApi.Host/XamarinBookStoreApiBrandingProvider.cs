using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace XamarinBookStoreApi
{
    [Dependency(ReplaceServices = true)]
    public class XamarinBookStoreApiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "XamarinBookStoreApi";
    }
}
