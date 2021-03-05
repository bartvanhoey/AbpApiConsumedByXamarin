using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBookStoreApp.ViewModels;

namespace XamarinBookStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        BookDetailViewModel _viewModel;

        public BookDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel= new BookDetailViewModel();
            CheckHasDeleteBookPermission();
        }

        async void CheckHasDeleteBookPermission()
        {
            try
            {
                _viewModel.HasDeleteBookPermission = await _viewModel.IsGrantedAsync(Permissions.Books.Delete);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                throw new Exception(ex.Message);
            }

        }
    }
}