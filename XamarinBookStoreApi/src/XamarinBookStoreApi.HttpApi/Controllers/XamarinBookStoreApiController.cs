using XamarinBookStoreApi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace XamarinBookStoreApi.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class XamarinBookStoreApiController : AbpController
    {
        protected XamarinBookStoreApiController()
        {
            LocalizationResource = typeof(XamarinBookStoreApiResource);
        }
    }
}