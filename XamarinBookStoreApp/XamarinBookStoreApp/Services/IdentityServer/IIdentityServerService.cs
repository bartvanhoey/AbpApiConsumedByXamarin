using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public interface IIdentityServerService
    {
        Task<string> GetAccessTokenAsync();
        Task<bool> LoginAysnc();
        Task<bool> LogoutAsync();
    }
}
