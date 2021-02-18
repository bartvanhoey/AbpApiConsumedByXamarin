using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public interface IIdentityServerService
    {
        OidcClient GetOidcClient();
    }
}
