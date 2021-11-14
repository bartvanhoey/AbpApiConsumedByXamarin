using AbpApi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpApi.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpApiController : AbpController
    {
        protected AbpApiController()
        {
            LocalizationResource = typeof(AbpApiResource);
        }
    }
}