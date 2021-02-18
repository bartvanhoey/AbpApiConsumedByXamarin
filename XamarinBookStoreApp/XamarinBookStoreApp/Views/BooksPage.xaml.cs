
using Xamarin.Forms;
using XamarinBookStoreApp.ViewModels;

namespace XamarinBookStoreApp.Views
{
    public partial class BooksPage : ContentPage
    {
        BooksViewModel _viewModel;

        public BooksPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new BooksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}