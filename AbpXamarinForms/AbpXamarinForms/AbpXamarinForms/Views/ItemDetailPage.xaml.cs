using AbpXamarinForms.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AbpXamarinForms.Views
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