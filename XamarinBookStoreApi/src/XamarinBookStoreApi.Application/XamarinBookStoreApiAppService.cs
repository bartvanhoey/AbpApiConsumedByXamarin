using System;
using System.Collections.Generic;
using System.Text;
using XamarinBookStoreApi.Localization;
using Volo.Abp.Application.Services;

namespace XamarinBookStoreApi
{
    /* Inherit your application services from this class.
     */
    public abstract class XamarinBookStoreApiAppService : ApplicationService
    {
        protected XamarinBookStoreApiAppService()
        {
            LocalizationResource = typeof(XamarinBookStoreApiResource);
        }
    }
}
