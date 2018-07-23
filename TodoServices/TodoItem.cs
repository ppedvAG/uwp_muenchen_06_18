using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace TodoServices
{
    public class TodoItem : INotifyPropertyChanged
    {
        //muss gesetzt werden damit statische Properties auch serialisiert werden
        [JsonProperty]
        public static int LastID { get; set; } = -1;

        private int _id;

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public string Description { get; set; }


        private DateTime _reminderDate;
        public DateTime ReminderDate
        {
            get
            {
                return _reminderDate;
            }
            set
            {
                if (_reminderDate != value)
                {
                    //_reminderDate = value > DateTime.Now ? value : _reminderDate;

                    if (value <= DateTime.Now)
                    {
                        throw new Exception("Darf nicht in der Vergangenheit liegen!");
                    }

                    _reminderDate = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReminderDate)));
                }
            }
        }


        private Color _categoryColor;

        public Color CategoryColor
        {
            get { return _categoryColor; }
            set
            {
                if (_categoryColor.Equals(value))
                    return;
                _categoryColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryColor)));
            }
        }

        public string ImageLink { get; set; }

        private bool _favorite;

        public bool Favorite
        {
            get { return _favorite; }
            set
            {
                _favorite = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Favorite)));
            }
        }


        public TodoItem(string title, string description = "...", DateTime reminderDate = default(DateTime), Color categoryColor = default(Color), string imageLink = "", bool favorite = false)
        {
            Title = title;
            Description = description;
            try
            {
                ReminderDate = reminderDate;
            }
            catch(Exception)
            {
                ReminderDate = DateTime.Now + TimeSpan.FromDays(7);
                favorite = false;
            }
            
            CategoryColor = categoryColor;
            CategoryColor = Color.Aqua;
            ImageLink = string.IsNullOrEmpty(imageLink) ? null : imageLink;
            Favorite = favorite;
            LastID++;
            ID = LastID;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
