using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public interface IIdentityServerService
    {
        Task<string> GetAccessTokenAsync();
        Task<bool> LoginAysnc();
        Task LogoutAsync();
    }
}
