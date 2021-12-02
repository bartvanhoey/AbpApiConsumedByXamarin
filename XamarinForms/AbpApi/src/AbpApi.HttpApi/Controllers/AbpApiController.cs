using AbpApi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpApi.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpApiController : AbpControllerBase
    {
        protected AbpApiController()
        {
            LocalizationResource = typeof(AbpApiResource);
        }
    }
}