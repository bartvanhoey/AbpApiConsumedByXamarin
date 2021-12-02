using System;
using System.Collections.Generic;
using System.Text;
using AbpApi.Localization;
using Volo.Abp.Application.Services;

namespace AbpApi
{
    /* Inherit your application services from this class.
     */
    public abstract class AbpApiAppService : ApplicationService
    {
        protected AbpApiAppService()
        {
            LocalizationResource = typeof(AbpApiResource);
        }
    }
}
