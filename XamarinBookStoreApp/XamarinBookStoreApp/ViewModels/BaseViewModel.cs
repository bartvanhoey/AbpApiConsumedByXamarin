using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBookStoreApp.Services.Books;
using XamarinBookStoreApp.Services.Books.Dtos;

namespace XamarinBookStoreApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IBooksDataStore<BookDto> BooksDataStore => DependencyService.Get<IBooksDataStore<BookDto>>();
        protected IBooksService BooksService => DependencyService.Get<IBooksService>();
      

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            bool.TryParse(await SecureStorage.GetAsync(permissionName), out bool result);
            return result;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
