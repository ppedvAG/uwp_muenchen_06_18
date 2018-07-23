using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksToDo.Controls
{
    public sealed partial class DateTimePicker : UserControl, INotifyPropertyChanged
    {

        public event EventHandler DateChanged;
    
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(default(DateTime), DatePropertyChangedCallback));

        //wird immer aufgerufen wenn sich die DateProperty verändert
        private static void DatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is DateTimePicker picker)
            {
                picker.PropertyChanged -= picker.DateTimePicker_PropertyChanged;
                if (args.NewValue is DateTime date)
                {
                    try
                    {
                        picker.DatePickerValue = new DateTimeOffset(date);
                        picker.TimePickerValue = new TimeSpan(date.Hour, date.Minute, date.Second);
                    }
                    catch (Exception)
                    {

                    }
                }
                picker.PropertyChanged += picker.DateTimePicker_PropertyChanged;
            }
        }

        public DateTimePicker()
        {
            this.InitializeComponent();
            PropertyChanged += DateTimePicker_PropertyChanged;
        }

        //Date-Property aktualisiern bei jeder Änderung von DatePickerValue oder TimePickerValue
        private void DateTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DateTime oldDate = Date;
            try
            {
                Date = new DateTime(DatePickerValue.Year, DatePickerValue.Month, DatePickerValue.Day, TimePickerValue.Hours, TimePickerValue.Minutes, TimePickerValue.Seconds);
                DateChanged?.Invoke(this, EventArgs.Empty);
            }
            catch(Exception)
            {
                Date = oldDate;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTimeOffset _datePickerValue;
        public DateTimeOffset DatePickerValue
        {
            get
            {
                return _datePickerValue;
            }
            set
            {
                if (_datePickerValue != value)
                {
                    _datePickerValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DatePickerValue)));
                }
            }
        }

        private TimeSpan _timePickerValue;
        public TimeSpan TimePickerValue
        {
            get
            {
                return _timePickerValue;
            }
            set
            {
                if (_timePickerValue != value)
                {
                    _timePickerValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimePickerValue)));
                }
            }
        }
    }
}
