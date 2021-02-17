
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.ViewModels;

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