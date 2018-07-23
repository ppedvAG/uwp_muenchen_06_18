using System;

using BooksToDo.ViewModels;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BooksToDo.Views
{
    public sealed partial class TodoItemsPage : Page
    {
        private TodoItemsViewModel _viewModel = new TodoItemsViewModel();

        public TodoItemsViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        public TodoItemsPage()
        {
            InitializeComponent();
            ViewModel.LoadingComplete += ViewModel_LoadingComplete;
        }

        private void ViewModel_LoadingComplete(object sender, EventArgs e)
        {
            progressBar1.Visibility = Visibility.Collapsed;
        }
    }
}
