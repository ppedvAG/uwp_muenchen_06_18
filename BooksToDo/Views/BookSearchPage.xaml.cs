using System;

using BooksToDo.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace BooksToDo.Views
{
    public sealed partial class BookSearchPage : Page
    {
        public BookSearchViewModel ViewModel { get; } = new BookSearchViewModel();

        public BookSearchPage()
        {
            InitializeComponent();
            ViewModel.ShowResultPage += ViewModel_ShowResultPage;
            ViewModel.ShowAlert += ViewModel_ShowAlert;
        }

        private void ViewModel_ShowAlert(object sender, string message)
        {
            new MessageDialog(message).ShowAsync().AsTask();
        }

        private void ViewModel_ShowResultPage(object sender, EventArgs e)
        {
            PivotItems.SelectedIndex = 1;
        }
    }
}
