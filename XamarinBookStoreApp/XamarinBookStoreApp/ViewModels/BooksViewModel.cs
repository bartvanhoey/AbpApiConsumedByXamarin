using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Models;
using XamarinBookStoreApp.Views;

namespace XamarinBookStoreApp.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        private Book _selectedBook;

        public ObservableCollection<Book> Books { get; }
        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<Book> BookTapped { get; }

        public BooksViewModel()
        {
            Title = "Browse";
            Books = new ObservableCollection<Book>();
            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());

            BookTapped = new Command<Book>(OnBookSelected);

            AddBookCommand = new Command(OnAddBook);
        }

        async Task ExecuteLoadBooksCommand()
        {
            IsBusy = true;

            try
            {
                Books.Clear();
                var books = await BooksDataStore.GetBooksAsync(true);
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedBook = null;
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetProperty(ref _selectedBook, value);
                OnBookSelected(value);
            }
        }

        private async void OnAddBook(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewBookPage));
            //Task.CompletedTask;
        }

        async void OnBookSelected(Book Book)
        {
            if (Book == null)
                return;

            // This will push the BookDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.BookId)}={Book.Id}");
        }
    }
}