
using System;
using Xamarin.Essentials;
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
            RemoveAddBookToolbarWhenNoCreateBookPermission();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void RemoveAddBookToolbarWhenNoCreateBookPermission()
        {
            try
            {

                bool.TryParse(await SecureStorage.GetAsync(Permissions.Books.Create), out bool hasCreateBookPermission);
                if (!hasCreateBookPermission) ToolbarItems.Remove(AddBookToolBarItem);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                throw new Exception(ex.Message);
            }

        }
    }
}