using Volo.Abp.Bundling;

namespace XamarinBookStoreApi.Blazor
{
    public class XamarinBookStoreApiBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}