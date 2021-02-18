using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public interface IIdentityServerService
    {
        //OidcClient GetOidcClient();
        Task<bool> LoginAysnc();
    }
}
