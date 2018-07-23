using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BooksToDo.Helpers
{
    public class DateTimeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is DateTime date)
            {
                    return $"{date.ToShortDateString()} {date.Hour}:{date.Minute}";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return default(DateTime);
        }
    }
}
