using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBookStoreApp.Views;

namespace XamarinBookStoreApp.ViewModels
{
    public class IdentityConnectViewModel : BaseViewModel
    {
        public ICommand ConnectorCommand { get; set; }

        public IdentityConnectViewModel()
        {
            Title = "BookStore API CONNECTOR";
            ConnectorCommand = new Command(OnConnectorClicked);
        }

        private async void OnConnectorClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
