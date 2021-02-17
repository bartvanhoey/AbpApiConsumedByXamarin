using System.ComponentModel;
using Xamarin.Forms;
using XamarinBookStoreApp.ViewModels;

namespace XamarinBookStoreApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}