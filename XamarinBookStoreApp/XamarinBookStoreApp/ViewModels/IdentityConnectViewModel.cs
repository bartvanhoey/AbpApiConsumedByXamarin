using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.IdentityServer;

namespace XamarinBookStoreApp.ViewModels
{
    public partial class IdentityConnectViewModel : BaseViewModel
    {
        IIdentityServerService IdentityService => DependencyService.Get<IIdentityServerService>();
      
        public Command LoginIdentityServerCommand { get; }
        public Command LogoutIdentityServerCommand { get; }

        public IdentityConnectViewModel()
        {
            Title = "IdentityServer";
            LoginIdentityServerCommand = new Command(async () => await LoginIdentityServerAsync());
            LogoutIdentityServerCommand = new Command(async () => await LogoutToIdentityServerAsync());
        }

        private async Task LogoutToIdentityServerAsync()
        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                               await IdentityService.LogoutAsync();
        }

        private async Task LoginIdentityServerAsync()
        {
            var loginResult = await IdentityService.LoginAysnc();
            if (!loginResult) return;

            await Shell.Current.GoToAsync("//BooksPage");
        }
    }
}
