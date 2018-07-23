using BooksToDo.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoServices;
using Windows.Storage;

namespace BooksToDo.Services
{
    public class TodoStorageService
    {
        const string TODO_LIST_KEY = "TodoList";
        const string STORAGE_CONTAINER_V1_KEY = "V1";


        private static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented
        };

        public static async void SaveTodos()
        {
            var container = ApplicationData.Current.LocalSettings.CreateContainer(STORAGE_CONTAINER_V1_KEY, ApplicationDataCreateDisposition.Always);

            await container.SaveAsync<ObservableCollection<TodoItem>>(TODO_LIST_KEY, TodoItemManager.ItemList);
        }

        public static async Task LoadTodos()
        {
            if(ApplicationData.Current.LocalSettings.Containers.TryGetValue(STORAGE_CONTAINER_V1_KEY, out ApplicationDataContainer container)) {
                if(container.Values.TryGetValue(TODO_LIST_KEY, out object obj))
                {
                    TodoItemManager.ItemList = await container.ReadAsync<ObservableCollection<TodoItem>>(TODO_LIST_KEY);
                }

            }
        }
    }
}
