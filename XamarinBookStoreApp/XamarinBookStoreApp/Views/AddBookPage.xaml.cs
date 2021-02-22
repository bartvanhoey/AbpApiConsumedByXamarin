
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.ViewModels;

namespace XamarinBookStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookPage : ContentPage
    {
        public BookDto Book{ get; set; }

        public AddBookPage()
        {
            InitializeComponent();
            BindingContext = new AddBookViewModel();
        }
    }
}