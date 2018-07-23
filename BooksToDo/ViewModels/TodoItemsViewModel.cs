using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using BooksToDo.Views;
using BooksToDo.Services;
using TodoServices;
using Microsoft.Toolkit.Uwp.UI.Controls;
using BooksToDo.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Threading;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Microsoft.Toolkit.Uwp.Notifications;

namespace BooksToDo.ViewModels
{
    public class TodoItemsViewModel : Observable
    {

        //Properties
        private ObservableCollection<TodoItem> _todoList;

        public ObservableCollection<TodoItem> TodoList
        {
            get { return _todoList; }
            set { Set(ref _todoList, value); }
        }

        private TodoItem _selectedItem;
        public TodoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
                DeleteTodoCommand?.OnCanExecuteChanged();
            }
        }

        private string _noSelectionText = "Todos werden geladen...";

        public string NoSelectionText
        {
            get { return _noSelectionText; }
            set
            {
                Set(ref _noSelectionText, value);
            }
        }


        //Commands
        public RelayCommand AddTodoCommand { get; set; }
        public RelayCommand DeleteTodoCommand { get; set; }
        public RelayCommand NextTodoCommand { get; set; }
        public RelayCommand PreviewTodoCommand { get; set; }
        public RelayCommand SaveTodosCommand { get; set; }
        public RelayCommand<object> ToggleFavoriteCommand { get; set; }

        public event EventHandler LoadingComplete;


        public TodoItemsViewModel()
        {
            TodoList = new ObservableCollection<TodoItem>();
            AddTodoCommand = new RelayCommand(() =>
            {
                TodoList.Add(new TodoItem($"Neues ToDo {TodoItem.LastID + 2}", "...", DateTime.Now + TimeSpan.FromDays(1)));
                SelectedItem = TodoList[TodoList.Count - 1];
                DeleteTodoCommand.OnCanExecuteChanged();
            });
            DeleteTodoCommand = new RelayCommand(() =>
            {
                TodoList.Remove(SelectedItem);
                DeleteTodoCommand.OnCanExecuteChanged();
            },
            () => SelectedItem != null
            );
            NextTodoCommand = new RelayCommand(() =>
            {
                if (TodoList.IndexOf(SelectedItem) < TodoList.Count - 1)
                {
                    SelectedItem = TodoList[TodoList.IndexOf(SelectedItem) + 1];
                }
                else
                {
                    SelectedItem = TodoList[0];
                }
            });
            PreviewTodoCommand = new RelayCommand(() =>
            {
                if (TodoList.IndexOf(SelectedItem) > 0)
                {
                    SelectedItem = TodoList[TodoList.IndexOf(SelectedItem) - 1];
                }
                else
                {
                    SelectedItem = TodoList.Last();
                }
            });
            SaveTodosCommand = new RelayCommand(() =>
            {
                TodoStorageService.SaveTodos();
            });
            ToggleFavoriteCommand = new RelayCommand<object>(HandleNotifications);

            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await TodoStorageService.LoadTodos();
                    await Task.Delay(500);
                }
                catch (Exception exp)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        NoSelectionText = exp.Message;
                        LoadingComplete?.Invoke(this, EventArgs.Empty);
                    });
                    return;
                }
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     this.TodoList = TodoItemManager.ItemList;
                     NoSelectionText = TodoList.Count > 0 ? "Wähle ein Todo aus oder erstelle ein Neues!" : "Erstelle dein erstes Todo!";
                     LoadingComplete?.Invoke(this, EventArgs.Empty);
                 });
            });
        }

        private string GetToastIDFromTodoItem(TodoItem item)
        {
            return $"Erinnerung {SelectedItem.ID}";
        }

        private void HandleNotifications(object favoriteCheckBoxOn)
        {
            SaveTodosCommand.Execute(null);

            //Schon vorhandene Notifications für das TodoItem löschen
            foreach (var item in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                if (item.Id == GetToastIDFromTodoItem(SelectedItem))
                {
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(item);
                }
            }

            if (SelectedItem.Favorite)
            {
                var content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                                {
                                    new AdaptiveText()
                                    {
                                        Text = "TodoItem Erinnerung"
                                    },

                                    new AdaptiveText()
                                    {
                                         Text = $"\"{SelectedItem.Title}\" ist überfällig!"
                                    }
                                }
                        }
                    },

                    Actions = new ToastActionsCustom()
                    {
                        Buttons =
                            {
                                //Wird auf dem Button geklickt, wird die App mit dem Argument "TodoErinnerung" gestartet
                                //Darauf kann in der OnActivated-Methode in App.xaml.cs reagiert werden.
                                //https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/send-local-toast
                                new ToastButton("OK", "TodoErinnerung")
                                {
                                    ActivationType = ToastActivationType.Foreground,
                                },

                                new ToastButtonDismiss("Cancel")
                            }
                    }
                };

                var sToast = new ScheduledToastNotification(content.GetXml(), SelectedItem.ReminderDate);
                sToast.Id = GetToastIDFromTodoItem(SelectedItem);
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(sToast);

                if (favoriteCheckBoxOn is string && Convert.ToBoolean(favoriteCheckBoxOn))
                {
                    new MessageDialog($"Die Erinnerung erfolg in {(SelectedItem.ReminderDate - DateTime.Now).TotalSeconds} Sekunden").ShowAsync().AsTask();
                }
            }
        }
    }
}
