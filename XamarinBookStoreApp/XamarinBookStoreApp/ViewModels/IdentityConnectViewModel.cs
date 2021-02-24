using System.Threading.Tasks;
using Xamarin.Forms;
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
            await Shell.Current.GoToAsync("//IdentityConnectPage");

        }

        private async Task LoginIdentityServerAsync()
        {
            var loginResult = await IdentityService.LoginAysnc();
            if (!loginResult) return;

            await Shell.Current.GoToAsync("//BooksPage");
        }
    }
}
