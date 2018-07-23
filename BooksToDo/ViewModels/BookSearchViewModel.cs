using System;
using System.Collections.Generic;
using System.Windows.Input;
using BooksToDo.Helpers;
using TodoServices;

namespace BooksToDo.ViewModels
{
    public class BookSearchViewModel : Observable
    {

        public string _bookName = "UWP";


        public string BookName
        {
            get { return _bookName; }
            set
            {
                Set(ref _bookName, value);
                SearchBookCommand.OnCanExecuteChanged();
            }
        }

        public RelayCommand SearchBookCommand { get; set; }

        private List<Book> _books;

        public List<Book> Books
        {
            get { return _books; }
            set { Set(ref _books, value); }
        }

        public BookSearchViewModel()
        {
            SearchBookCommand = new RelayCommand(SearchForBooks, () => !string.IsNullOrWhiteSpace(BookName));
        }

        public event EventHandler ShowResultPage;
        public event EventHandler<string> ShowAlert;

        public async void SearchForBooks()
        {
            //Wenn die aufzurufunde asynchrone Methode Request eine await-Anweisung enthält,
            //dann darf an dieser Stelle nicht mit .Result die Methode zur synchronen Ausführung gezwungen werden,
            //da ansonsten der GUI-Thread blockiert und nicht mehr aufs Internet zugegriffen werden kann.
            var result = await GoogleBooksAPIService.Request(BookName);
            if(result.books.Count > 0)
            {
                Books = result.books;
                ShowResultPage?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if(result.errorMessage != string.Empty)
                {
                    ShowAlert(this, result.errorMessage);
                }
                else
                {
                    ShowAlert(this, "keine Ergebnisse für diesen Suchbegriff!");
                }
            }
        }
    }
}
