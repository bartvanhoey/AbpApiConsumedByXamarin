
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.ViewModels;

namespace XamarinBookStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookPage : ContentPage
    {
        public Book Book{ get; set; }

        public AddBookPage()
        {
            InitializeComponent();
            BindingContext = new AddBookViewModel();
        }
    }
}