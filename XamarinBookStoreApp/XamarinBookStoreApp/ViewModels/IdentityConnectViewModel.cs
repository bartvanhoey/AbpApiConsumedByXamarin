using System;
using Xamarin.Forms;

namespace XamarinBookStoreApp.ViewModels
{
    public class IdentityConnectViewModel : BaseViewModel
    {
        public Command ConnectToIdentityServerCommand { get; }

        public IdentityConnectViewModel()
        {
            ConnectToIdentityServerCommand = new Command(ConnectToIdentityServer);
        }

        private void ConnectToIdentityServer(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
