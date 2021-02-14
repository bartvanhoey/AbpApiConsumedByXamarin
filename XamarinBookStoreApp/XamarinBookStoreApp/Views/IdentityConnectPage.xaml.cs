using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinBookStoreApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBookStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdentityConnectPage : ContentPage
    {
        public IdentityConnectPage()
        {
            InitializeComponent();
            this.BindingContext = new IdentityConnectViewModel();

        }
    }
}