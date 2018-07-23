using BooksToDo.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoServices;

namespace BooksToDo.Models
{
    public class Todo : Observable
    {

        private TodoItem _original;
      
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        public string Description { get; set; }

        public DateTime ReminderDate { get; set; }


        private Color _categoryColor;

        public Color CategoryColor
        {
            get { return _categoryColor; }
            set
            {
                Set(ref _categoryColor, value);
            }
        }

        public string ImageLink { get; set; }

        public bool Favorite { get; set; }

        public Todo(string title, string description = "...", DateTime reminderDate = default(DateTime), Color categoryColor = default(Color), string imageLink = "", bool favorite = false)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            CategoryColor = categoryColor;
            CategoryColor = Color.Aqua;
            ImageLink = string.IsNullOrEmpty(imageLink) ? null : imageLink;
            Favorite = favorite;
        }
    }
}
