using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TodoServices
{
    public class TodoItemManager
    {
        public static ObservableCollection<TodoItem> ItemList { get; set; } = new ObservableCollection<TodoItem>();
    }
}
