using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace XamarinBookStoreApi
{
    [Dependency(ReplaceServices = true)]
    public class XamarinBookStoreApiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "XamarinBookStoreApi";
    }
}
