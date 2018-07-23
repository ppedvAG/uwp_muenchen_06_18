using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using BooksToDo.Helpers;
using BooksToDo.Services;
using BooksToDo.Views;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BooksToDo.ViewModels
{
    public class ShellViewModel : Observable
    {
        private NavigationView _navigationView;
        private NavigationViewItem _selected;
        private ICommand _itemInvokedCommand;

        public NavigationViewItem Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        public ShellViewModel()
        {
            string backgroundTask = "CheckTodos";

            return;

            //Background-Task registrieren
            foreach (var item in BackgroundTaskRegistration.AllTasks)
            {
                if (item.Value.Name == "CheckTodos")
                {
                    item.Value.Unregister(true);
                }
            }

            var builder = new BackgroundTaskBuilder();
            builder.Name = backgroundTask;
            builder.TaskEntryPoint = "TodoBackground.CheckTodos";
            builder.SetTrigger(new TimeTrigger(15, true));

            var state = BackgroundExecutionManager.RequestAccessAsync().AsTask().Result;
            switch (state)
            {
                case BackgroundAccessStatus.AlwaysAllowed:
                    break;
                case BackgroundAccessStatus.AllowedSubjectToSystemPolicy:
                    break;
                case BackgroundAccessStatus.DeniedBySystemPolicy:
                case BackgroundAccessStatus.DeniedByUser:
                    return;
            }

            BackgroundTaskRegistration registeredTask = builder.Register();
            registeredTask.Completed += RegisteredTask_Completed;
            registeredTask.Progress += RegisteredTask_Progress;
            new MessageDialog("Hallo").ShowAsync().AsTask();
        }

        private void RegisteredTask_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            if (Window.Current == null)
                return;
            if (Window.Current.Content == null)
                return;
            if (Window.Current.Content.Dispatcher == null)
                return;

            Window.Current.Content.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                new MessageDialog("Progress: ").ShowAsync().AsTask();
            }).AsTask();
        }

        private void RegisteredTask_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            //new MessageDialog("Completed: " + sender.Name).ShowAsync().AsTask();
        }

        public void Initialize(Frame frame, NavigationView navigationView)
        {
            _navigationView = navigationView;
            NavigationService.Frame = frame;
            NavigationService.Navigated += Frame_Navigated;
        }

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage));
                return;
            }

            var item = _navigationView.MenuItems
                            .OfType<NavigationViewItem>()
                            .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);
            var pageType = item.GetValue(NavHelper.NavigateToProperty) as Type;
            NavigationService.Navigate(pageType);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.SourcePageType == typeof(SettingsPage))
            {
                Selected = _navigationView.SettingsItem as NavigationViewItem;
                return;
            }

            Selected = _navigationView.MenuItems
                            .OfType<NavigationViewItem>()
                            .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, e.SourcePageType));
        }

        private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var pageType = menuItem.GetValue(NavHelper.NavigateToProperty) as Type;
            return pageType == sourcePageType;
        }
    }
}
