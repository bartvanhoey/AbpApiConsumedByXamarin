using System.Threading.Tasks;

namespace XamarinBookStoreApp.Services.IdentityServer
{
    public interface IIdentityService
    {
        Task<string> GetAccessTokenAsync();
        Task<bool> LoginAsync();
        Task<bool> LogoutAsync();
    }
}
