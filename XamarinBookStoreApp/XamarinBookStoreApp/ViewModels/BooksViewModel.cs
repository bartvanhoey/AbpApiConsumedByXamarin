using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBookStoreApp.Services.Books.Dtos;
using XamarinBookStoreApp.Views;

namespace XamarinBookStoreApp.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {

        private BookDto _selectedBook;
        public ObservableCollection<BookDto> Books { get; }
        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<BookDto> BookTapped { get; }

        public BooksViewModel()
        {
            Title = "Books List";
            Books = new ObservableCollection<BookDto>();
            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());
            BookTapped = new Command<BookDto>(OnBookSelected);
            AddBookCommand = new Command(OnAddBook);
        }

        async Task ExecuteLoadBooksCommand()
        {
            IsBusy = true;
            try
            {
                var books = await BooksService.GetListAsync();
                Books.Clear();
                foreach (var book in books) Books.Add(book);
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

        public BookDto SelectedBook
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
            await Shell.Current.GoToAsync(nameof(AddBookPage));
            //Task.CompletedTask;
        }

        async void OnBookSelected(BookDto Book)
        {
            if (Book == null)
                return;

            // This will push the BookDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.BookId)}={Book.Id}");
        }
    }
}